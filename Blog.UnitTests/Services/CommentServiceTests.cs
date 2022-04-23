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
    public class CommentServiceTests
    {
        private readonly Mock<ICommentRepository> _mockCommentRepository;
        private readonly ICommentService _commentService;

        private static Faker _faker = new Faker("pt_BR");

        public CommentServiceTests()
        {
            _mockCommentRepository = new Mock<ICommentRepository>();
            _commentService = new CommentService(_mockCommentRepository.Object);
        }

        [Fact]
        public async Task GetComments_ShouldReturnCommentList_WhenEverythingIsOk()
        {
            //Arrange
            _mockCommentRepository
                .Setup(x => x.GetComments(It.IsAny<int>()))
                .ReturnsAsync(new List<Comment>());

            // Act
            var comments = await _commentService.GetComments(It.IsAny<int>());

            // Assert
            comments.Should().BeOfType<List<CommentDao>>();
            _mockCommentRepository.Verify(x => x.GetComments(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetComment_ShouldReturnComment_WhenEverythingIsOk()
        {
            //Arrange
            _mockCommentRepository
                .Setup(x => x.GetComment(It.IsAny<int>()))
                .ReturnsAsync(new Comment());

            // Act
            var comment = await _commentService.GetComment(It.IsAny<int>());

            // Assert
            comment.Should().BeOfType<CommentDao>();
            _mockCommentRepository.Verify(x => x.GetComment(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetComment_ShouldReturnException_WhenCommentNotFound()
        {
            //Arrange
            _mockCommentRepository
                .Setup(x => x.GetComment(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Comment>());

            // Act
            Func<Task> action = async () => await _commentService.GetComment(It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockCommentRepository.Verify(x => x.GetComment(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task InsertComment_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockCommentRepository
                .Setup(x => x.InsertComment(It.IsAny<Comment>()))
                .ReturnsAsync(true);

            // Act
            var action = await _commentService.InsertComment(It.IsAny<Comment>());

            // Assert
            action.Should().BeTrue();
            _mockCommentRepository.Verify(x => x.InsertComment(It.IsAny<Comment>()), Times.Once);
        }

        [Fact]
        public async Task UpdateComment_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            var comment = new Comment()
            {
                CommentId = _faker.Random.Int(1, 10)
            };

            _mockCommentRepository
                .Setup(x => x.GetComment(It.IsAny<int>()))
                .ReturnsAsync(new Comment());

            _mockCommentRepository
                .Setup(x => x.UpdateComment(It.IsAny<Comment>()))
                .ReturnsAsync(true);

            // Act
            var action = await _commentService.UpdateComment(comment, true, It.IsAny<int>());

            // Assert
            action.Should().BeTrue();
            _mockCommentRepository.Verify(x => x.GetComment(It.IsAny<int>()), Times.Once);
            _mockCommentRepository.Verify(x => x.UpdateComment(It.IsAny<Comment>()), Times.Once);
        }

        [Fact]
        public async Task UpdateComment_ShouldReturnException_WhenCommentNotFound()
        {
            //Arrange
            var comment = new Comment()
            {
                CommentId = _faker.Random.Int(1, 10)
            };

            _mockCommentRepository
                .Setup(x => x.GetComment(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Comment>());

            // Act
            Func<Task> action = async () => await _commentService.UpdateComment(comment, true, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockCommentRepository.Verify(x => x.GetComment(It.IsAny<int>()), Times.Once);
            _mockCommentRepository.Verify(x => x.UpdateComment(It.IsAny<Comment>()), Times.Never);
        }

        [Fact]
        public async Task UpdateComment_ShouldReturnException_WhenUserNotHavePermission()
        {
            //Arrange
            var commentFound = new Comment()
            {
                User = new User()
                {
                    UserId = _faker.Random.Int(1, 10)
                }
            };

            var comment = new Comment()
            {
                CommentId = _faker.Random.Int(1, 10)
            };

            _mockCommentRepository
                .Setup(x => x.GetComment(It.IsAny<int>()))
                .ReturnsAsync(commentFound);

            // Act
            Func<Task> action = async () => await _commentService.UpdateComment(comment, false, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockCommentRepository.Verify(x => x.GetComment(It.IsAny<int>()), Times.Once);
            _mockCommentRepository.Verify(x => x.UpdateComment(It.IsAny<Comment>()), Times.Never);
        }

        [Fact]
        public async Task DeleteComment_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockCommentRepository
                .Setup(x => x.GetComment(It.IsAny<int>()))
                .ReturnsAsync(new Comment());

            _mockCommentRepository
                .Setup(x => x.DeleteComment(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var action = await _commentService.DeleteComment(It.IsAny<int>(), true, It.IsAny<int>());

            // Assert
            action.Should().BeTrue();
            _mockCommentRepository.Verify(x => x.GetComment(It.IsAny<int>()), Times.Once);
            _mockCommentRepository.Verify(x => x.DeleteComment(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteComment_ShouldReturnException_WhenCommentNotFound()
        {
            //Arrange
            var comment = new Comment()
            {
                CommentId = _faker.Random.Int(1, 10)
            };

            _mockCommentRepository
                .Setup(x => x.GetComment(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Comment>());

            // Act
            Func<Task> action = async () => await _commentService.DeleteComment(It.IsAny<int>(), true, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockCommentRepository.Verify(x => x.GetComment(It.IsAny<int>()), Times.Once);
            _mockCommentRepository.Verify(x => x.DeleteComment(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteComment_ShouldReturnException_WhenUserNotHavePermission()
        {
            //Arrange
            var commentFound = new Comment()
            {
                User = new User()
                {
                    UserId = _faker.Random.Int(1, 10)
                }
            };

            var comment = new Comment()
            {
                CommentId = _faker.Random.Int(1, 10)
            };

            _mockCommentRepository
                .Setup(x => x.GetComment(It.IsAny<int>()))
                .ReturnsAsync(commentFound);

            // Act
            Func<Task> action = async () => await _commentService.DeleteComment(It.IsAny<int>(), false, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockCommentRepository.Verify(x => x.GetComment(It.IsAny<int>()), Times.Once);
            _mockCommentRepository.Verify(x => x.DeleteComment(It.IsAny<int>()), Times.Never);
        }
    }
}
