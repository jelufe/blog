using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using Blog.Domain.Services;
using Bogus;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Blog.UnitTests.Services
{
    public class PostServiceTests
    {
        private readonly Mock<IPostRepository> _mockPostRepository;
        private readonly IPostService _postService;

        private static Faker _faker = new Faker("pt_BR");

        public PostServiceTests()
        {
            _mockPostRepository = new Mock<IPostRepository>();
            _postService = new PostService(_mockPostRepository.Object);
        }

        [Fact]
        public async Task GetPosts_ShouldReturnPostList_WhenEverythingIsOk()
        {
            //Arrange
            _mockPostRepository
                .Setup(x => x.GetPosts(It.IsAny<int>()))
                .ReturnsAsync(new List<Post>());

            // Act
            var posts = await _postService.GetPosts(It.IsAny<int>());

            // Assert
            posts.Should().BeOfType<List<PostDao>>();
            _mockPostRepository.Verify(x => x.GetPosts(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetPost_ShouldReturnPost_WhenEverythingIsOk()
        {
            //Arrange
            _mockPostRepository
                .Setup(x => x.GetPost(It.IsAny<int>()))
                .ReturnsAsync(new Post());

            // Act
            var post = await _postService.GetPost(It.IsAny<int>());

            // Assert
            post.Should().BeOfType<PostDao>();
            _mockPostRepository.Verify(x => x.GetPost(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetPost_ShouldReturnException_WhenPostNotFound()
        {
            //Arrange
            _mockPostRepository
                .Setup(x => x.GetPost(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Post>());

            // Act
            Func<Task> action = async () => await _postService.GetPost(It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockPostRepository.Verify(x => x.GetPost(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetComments_ShouldReturnComments_WhenEverythingIsOk()
        {
            //Arrange
            _mockPostRepository
                .Setup(x => x.GetPost(It.IsAny<int>()))
                .ReturnsAsync(new Post());

            _mockPostRepository
                .Setup(x => x.GetComments(It.IsAny<int>()))
                .ReturnsAsync(new List<Comment>());

            // Act
            var comments = await _postService.GetComments(It.IsAny<int>());

            // Assert
            comments.Should().BeOfType<List<CommentDao>>();
            _mockPostRepository.Verify(x => x.GetPost(It.IsAny<int>()), Times.Once);
            _mockPostRepository.Verify(x => x.GetComments(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetComments_ShouldReturnException_WhenPostNotFound()
        {
            //Arrange
            _mockPostRepository
                .Setup(x => x.GetPost(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Post>());

            // Act
            Func<Task> action = async () => await _postService.GetComments(It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockPostRepository.Verify(x => x.GetPost(It.IsAny<int>()), Times.Once);
            _mockPostRepository.Verify(x => x.GetComments(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task InsertPost_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockPostRepository
                .Setup(x => x.InsertPost(It.IsAny<Post>()))
                .ReturnsAsync(true);

            // Act
            var action = await _postService.InsertPost(It.IsAny<Post>());

            // Assert
            action.Should().BeTrue();
            _mockPostRepository.Verify(x => x.InsertPost(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public async Task UpdatePost_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            var post = new Post()
            {
                PostId = _faker.Random.Int(1, 10)
            };

            _mockPostRepository
                .Setup(x => x.GetPost(It.IsAny<int>()))
                .ReturnsAsync(new Post());

            _mockPostRepository
                .Setup(x => x.UpdatePost(It.IsAny<Post>()))
                .ReturnsAsync(true);

            // Act
            var action = await _postService.UpdatePost(post, true, It.IsAny<int>());

            // Assert
            action.Should().BeTrue();
            _mockPostRepository.Verify(x => x.GetPost(It.IsAny<int>()), Times.Once);
            _mockPostRepository.Verify(x => x.UpdatePost(It.IsAny<Post>()), Times.Once);
        }

        [Fact]
        public async Task UpdatePost_ShouldReturnException_WhenPostNotFound()
        {
            //Arrange
            var post = new Post()
            {
                PostId = _faker.Random.Int(1, 10)
            };

            _mockPostRepository
                .Setup(x => x.GetPost(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Post>());

            // Act
            Func<Task> action = async () => await _postService.UpdatePost(post, true, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockPostRepository.Verify(x => x.GetPost(It.IsAny<int>()), Times.Once);
            _mockPostRepository.Verify(x => x.UpdatePost(It.IsAny<Post>()), Times.Never);
        }

        [Fact]
        public async Task UpdatePost_ShouldReturnException_WhenUserNotHavePermission()
        {
            //Arrange
            var postFound = new Post()
            {
                User = new User() 
                {
                    UserId = _faker.Random.Int(1, 10)
                }
            };

            var post = new Post()
            {
                PostId = _faker.Random.Int(1, 10)
            };

            _mockPostRepository
                .Setup(x => x.GetPost(It.IsAny<int>()))
                .ReturnsAsync(postFound);

            // Act
            Func<Task> action = async () => await _postService.UpdatePost(post, false, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockPostRepository.Verify(x => x.GetPost(It.IsAny<int>()), Times.Once);
            _mockPostRepository.Verify(x => x.UpdatePost(It.IsAny<Post>()), Times.Never);
        }

        [Fact]
        public async Task DeletePost_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockPostRepository
                .Setup(x => x.GetPost(It.IsAny<int>()))
                .ReturnsAsync(new Post());

            _mockPostRepository
                .Setup(x => x.DeletePost(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var action = await _postService.DeletePost(It.IsAny<int>(), true, It.IsAny<int>());

            // Assert
            action.Should().BeTrue();
            _mockPostRepository.Verify(x => x.GetPost(It.IsAny<int>()), Times.Once);
            _mockPostRepository.Verify(x => x.DeletePost(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeletePost_ShouldReturnException_WhenPostNotFound()
        {
            //Arrange
            var post = new Post()
            {
                PostId = _faker.Random.Int(1, 10)
            };

            _mockPostRepository
                .Setup(x => x.GetPost(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Post>());

            // Act
            Func<Task> action = async () => await _postService.DeletePost(It.IsAny<int>(), true, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockPostRepository.Verify(x => x.GetPost(It.IsAny<int>()), Times.Once);
            _mockPostRepository.Verify(x => x.DeletePost(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeletePost_ShouldReturnException_WhenUserNotHavePermission()
        {
            //Arrange
            var postFound = new Post()
            {
                User = new User()
                {
                    UserId = _faker.Random.Int(1, 10)
                }
            };

            var post = new Post()
            {
                PostId = _faker.Random.Int(1, 10)
            };

            _mockPostRepository
                .Setup(x => x.GetPost(It.IsAny<int>()))
                .ReturnsAsync(postFound);

            // Act
            Func<Task> action = async () => await _postService.DeletePost(It.IsAny<int>(), false, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockPostRepository.Verify(x => x.GetPost(It.IsAny<int>()), Times.Once);
            _mockPostRepository.Verify(x => x.DeletePost(It.IsAny<int>()), Times.Never);
        }
    }
}
