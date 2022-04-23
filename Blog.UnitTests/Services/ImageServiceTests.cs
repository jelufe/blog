using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using Blog.Domain.Services;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Blog.UnitTests.Services
{
    public class ImageServiceTests
    {
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private readonly Mock<IImageRepository> _mockImageRepository;
        private readonly IImageService _imageService;

        private static Faker _faker = new Faker("pt_BR");

        public ImageServiceTests()
        {
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockImageRepository = new Mock<IImageRepository>();

            _imageService = new ImageService(
                _mockWebHostEnvironment.Object,
                _mockImageRepository.Object);
        }

        [Fact]
        public async Task GetImages_ShouldReturnImageList_WhenEverythingIsOk()
        {
            //Arrange
            _mockImageRepository
                .Setup(x => x.GetImages(It.IsAny<int>()))
                .ReturnsAsync(new List<Image>());

            // Act
            var images = await _imageService.GetImages(It.IsAny<int>());

            // Assert
            images.Should().BeOfType<List<ImageDao>>();
            _mockImageRepository.Verify(x => x.GetImages(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteImage_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockImageRepository
                .Setup(x => x.GetImage(It.IsAny<int>()))
                .ReturnsAsync(new Image());

            _mockImageRepository
                .Setup(x => x.DeleteImage(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var action = await _imageService.DeleteImage(It.IsAny<int>(), true, It.IsAny<int>());

            // Assert
            action.Should().BeTrue();
            _mockImageRepository.Verify(x => x.GetImage(It.IsAny<int>()), Times.Once);
            _mockImageRepository.Verify(x => x.DeleteImage(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteImage_ShouldReturnException_WhenImageNotFound()
        {
            //Arrange
            _mockImageRepository
                .Setup(x => x.GetImage(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Image>());

            // Act
            Func<Task> action = async () => await _imageService.DeleteImage(It.IsAny<int>(), true, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockImageRepository.Verify(x => x.GetImage(It.IsAny<int>()), Times.Once);
            _mockImageRepository.Verify(x => x.DeleteImage(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteImage_ShouldReturnException_WhenUserNotHavePermission()
        {
            //Arrange
            var image = new Image()
            {
                User = new User()
                {
                    UserId = _faker.Random.Int(1, 10)
                }
            };

            _mockImageRepository
                .Setup(x => x.GetImage(It.IsAny<int>()))
                .ReturnsAsync(image);

            _mockImageRepository
                .Setup(x => x.DeleteImage(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            Func<Task> action = async () => await _imageService.DeleteImage(It.IsAny<int>(), false, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockImageRepository.Verify(x => x.GetImage(It.IsAny<int>()), Times.Once);
            _mockImageRepository.Verify(x => x.DeleteImage(It.IsAny<int>()), Times.Never);
        }
    }
}
