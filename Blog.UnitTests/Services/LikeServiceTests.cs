using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using Blog.Domain.Services;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Blog.UnitTests.Services
{
    public class LikeServiceTests
    {
        private readonly Mock<ILikeRepository> _mockLikeRepository;
        private readonly ILikeService _likeService;

        public LikeServiceTests()
        {
            _mockLikeRepository = new Mock<ILikeRepository>();
            _likeService = new LikeService(_mockLikeRepository.Object);
        }

        [Fact]
        public async Task GetLike_ShouldReturnLike_WhenEverythingIsOk()
        {
            //Arrange
            _mockLikeRepository
                .Setup(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Like());

            // Act
            var like = await _likeService.GetLike(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            like.Should().BeOfType<LikeDao>();
            _mockLikeRepository.Verify(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetLike_ShouldReturnException_WhenLikeNotFound()
        {
            //Arrange
            _mockLikeRepository
                .Setup(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Like>());

            // Act
            Func<Task> action = async () => await _likeService.GetLike(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockLikeRepository.Verify(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task InsertLike_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockLikeRepository
                .Setup(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Like>());

            _mockLikeRepository
                .Setup(x => x.InsertLike(It.IsAny<Like>()))
                .ReturnsAsync(true);

            // Act
            var action = await _likeService.InsertLike(new Like(), It.IsAny<int>());

            // Assert
            action.Should().BeTrue();
            _mockLikeRepository.Verify(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _mockLikeRepository.Verify(x => x.InsertLike(It.IsAny<Like>()), Times.Once);
        }

        [Fact]
        public async Task InsertLike_ShouldReturnException_WhenLikeAlreadyExists()
        {
            //Arrange
            _mockLikeRepository
                .Setup(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Like());

            // Act
            Func<Task> action = async () => await _likeService.InsertLike(new Like(), It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockLikeRepository.Verify(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _mockLikeRepository.Verify(x => x.InsertLike(It.IsAny<Like>()), Times.Never);
        }

        [Fact]
        public async Task DeleteLike_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockLikeRepository
                .Setup(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Like());

            _mockLikeRepository
                .Setup(x => x.DeleteLike(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var action = await _likeService.DeleteLike(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            action.Should().BeTrue();
            _mockLikeRepository.Verify(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _mockLikeRepository.Verify(x => x.DeleteLike(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteLike_ShouldReturnException_WhenLikeNotFound()
        {
            //Arrange
            _mockLikeRepository
                .Setup(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Like>());

            // Act
            Func<Task> action = async () => await _likeService.DeleteLike(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockLikeRepository.Verify(x => x.GetLike(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            _mockLikeRepository.Verify(x => x.DeleteLike(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
    }
}
