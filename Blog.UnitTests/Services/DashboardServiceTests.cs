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
    public class DashboardServiceTests
    {
        private readonly Mock<IDashboardRepository> _mockDashboardRepository;
        private readonly IDashboardService _dashboardService;

        public DashboardServiceTests()
        {
            _mockDashboardRepository = new Mock<IDashboardRepository>();
            _dashboardService = new DashboardService(_mockDashboardRepository.Object);
        }

        [Fact]
        public async Task GetDashboardData_ShouldReturnDashboardData_WhenEverythingIsOk()
        {
            //Arrange
            _mockDashboardRepository
                .Setup(x => x.GetDashboardData())
                .ReturnsAsync(new Dashboard());

            // Act
            var dashboardData = await _dashboardService.GetDashboardData();

            // Assert
            dashboardData.Should().BeOfType<Dashboard>();
            _mockDashboardRepository.Verify(x => x.GetDashboardData(), Times.Once);
        }
    }
}
