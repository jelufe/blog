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
    public class VisualizationServiceTests
    {
        private readonly Mock<IVisualizationRepository> _mockVisualizationRepository;
        private readonly IVisualizationService _visualizationService;

        public VisualizationServiceTests()
        {
            _mockVisualizationRepository = new Mock<IVisualizationRepository>();
            _visualizationService = new VisualizationService(_mockVisualizationRepository.Object);
        }

        [Fact]
        public async Task GetVisualizationByUser_ShouldReturnVisualization_WhenEverythingIsOk()
        {
            //Arrange
            _mockVisualizationRepository
                .Setup(x => x.GetVisualizationByUser(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Visualization());

            // Act
            var visualization = await _visualizationService.GetVisualization(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            visualization.Should().BeOfType<VisualizationDao>();
            _mockVisualizationRepository.Verify(x => x.GetVisualizationByUser(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetVisualizationByUser_ShouldReturnException_WhenVisualizationNotFound()
        {
            //Arrange
            _mockVisualizationRepository
                .Setup(x => x.GetVisualizationByUser(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Visualization>());

            // Act
            Func<Task> action = async () => await _visualizationService.GetVisualization(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockVisualizationRepository.Verify(x => x.GetVisualizationByUser(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetVisualizationBySession_ShouldReturnVisualization_WhenEverythingIsOk()
        {
            //Arrange
            _mockVisualizationRepository
                .Setup(x => x.GetVisualizationBySession(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new Visualization());

            // Act
            var visualization = await _visualizationService.GetVisualization(It.IsAny<int>(), It.IsAny<string>());

            // Assert
            visualization.Should().BeOfType<VisualizationDao>();
            _mockVisualizationRepository.Verify(x => x.GetVisualizationBySession(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetVisualizationBySession_ShouldReturnException_WhenVisualizationNotFound()
        {
            //Arrange
            _mockVisualizationRepository
                .Setup(x => x.GetVisualizationBySession(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<Visualization>());

            // Act
            Func<Task> action = async () => await _visualizationService.GetVisualization(It.IsAny<int>(), It.IsAny<string>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockVisualizationRepository.Verify(x => x.GetVisualizationBySession(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task InsertVisualization_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockVisualizationRepository
                .Setup(x => x.InsertVisualization(It.IsAny<Visualization>()))
                .ReturnsAsync(true);

            // Act
            var action = await _visualizationService.InsertVisualization(new Visualization());

            // Assert
            action.Should().BeTrue();
            _mockVisualizationRepository.Verify(x => x.InsertVisualization(It.IsAny<Visualization>()), Times.Once);
        }

        [Fact]
        public async Task UpdateVisualization_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockVisualizationRepository
                .Setup(x => x.GetVisualization(It.IsAny<int>()))
                .ReturnsAsync(new Visualization());

            _mockVisualizationRepository
                .Setup(x => x.UpdateVisualization(It.IsAny<Visualization>()))
                .ReturnsAsync(true);

            // Act
            var action = await _visualizationService.UpdateVisualization(new Visualization());

            // Assert
            action.Should().BeTrue();
            _mockVisualizationRepository.Verify(x => x.GetVisualization(It.IsAny<int>()), Times.Once);
            _mockVisualizationRepository.Verify(x => x.UpdateVisualization(It.IsAny<Visualization>()), Times.Once);
        }

        [Fact]
        public async Task UpdateVisualization_ShouldReturnException_WhenVisualizationNotFound()
        {
            //Arrange
            _mockVisualizationRepository
                .Setup(x => x.GetVisualization(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Visualization>);

            // Act
            Func<Task> action = async () => await _visualizationService.UpdateVisualization(new Visualization());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockVisualizationRepository.Verify(x => x.GetVisualization(It.IsAny<int>()), Times.Once);
            _mockVisualizationRepository.Verify(x => x.UpdateVisualization(It.IsAny<Visualization>()), Times.Never);
        }
    }
}
