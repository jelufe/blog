using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using Blog.Domain.Services;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Blog.UnitTests.Services
{
    public class SharingServiceTests
    {
        private readonly Mock<ISharingRepository> _mockSharingRepository;
        private readonly ISharingService _sharingService;

        public SharingServiceTests()
        {
            _mockSharingRepository = new Mock<ISharingRepository>();
            _sharingService = new SharingService(_mockSharingRepository.Object);
        }

        [Fact]
        public async Task InsertSharing_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockSharingRepository
                .Setup(x => x.InsertSharing(It.IsAny<Sharing>()))
                .ReturnsAsync(true);

            // Act
            var action = await _sharingService.InsertSharing(new Sharing());

            // Assert
            action.Should().BeTrue();
            _mockSharingRepository.Verify(x => x.InsertSharing(It.IsAny<Sharing>()), Times.Once);
        }
    }
}
