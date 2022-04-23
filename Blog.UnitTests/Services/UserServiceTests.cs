using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using Blog.Domain.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Blog.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockUserRepository.Object);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnUserList_WhenEverythingIsOk()
        {
            //Arrange
            _mockUserRepository
                .Setup(x => x.GetUsers())
                .ReturnsAsync(new List<User>() { new User() });

            // Act
            var users = await _userService.GetUsers();

            // Assert
            users.Should().BeOfType<List<User>>();
            users.Should().NotBeEmpty();
            _mockUserRepository.Verify(x => x.GetUsers(), Times.Once);
        }

        [Fact]
        public async Task GetUser_ShouldReturnUser_WhenEverythingIsOk()
        {
            //Arrange
            _mockUserRepository
                .Setup(x => x.GetUser(It.IsAny<int>()))
                .ReturnsAsync(new User());

            // Act
            var user = await _userService.GetUser(It.IsAny<int>());

            // Assert
            user.Should().BeOfType<User>();
            user.Should().NotBeNull();
            _mockUserRepository.Verify(x => x.GetUser(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetUser_ShouldReturnException_WhenUserNotFound()
        {
            //Arrange
            _mockUserRepository
                .Setup(x => x.GetUser(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<User>());

            // Act
            Func<Task> action = async () => await _userService.GetUser(It.IsAny<int>());

            //Assert
            action.Should().ThrowAsync<Exception>();
            _mockUserRepository.Verify(x => x.GetUser(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task InsertUser_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockUserRepository
                .Setup(x => x.InsertUser(It.IsAny<User>()))
                .ReturnsAsync(true);

            // Act
            var action = await _userService.InsertUser(It.IsAny<User>());

            // Assert
            action.Should().BeTrue();
            _mockUserRepository.Verify(x => x.InsertUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockUserRepository
                .Setup(x => x.GetUser(It.IsAny<int>()))
                .ReturnsAsync(new User());

            _mockUserRepository
                .Setup(x => x.UpdateUser(It.IsAny<User>()))
                .ReturnsAsync(true);

            // Act
            var action = await _userService.UpdateUser(new User());

            // Assert
            action.Should().BeTrue();
            _mockUserRepository.Verify(x => x.GetUser(It.IsAny<int>()), Times.Once);
            _mockUserRepository.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnException_WhenUserNotFound()
        {
            //Arrange
            _mockUserRepository
                .Setup(x => x.GetUser(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<User>());

            // Act
            Func<Task> action = async () => await _userService.UpdateUser(new User());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockUserRepository.Verify(x => x.GetUser(It.IsAny<int>()), Times.Once);
            _mockUserRepository.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockUserRepository
                .Setup(x => x.GetUser(It.IsAny<int>()))
                .ReturnsAsync(new User());

            _mockUserRepository
                .Setup(x => x.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var action = await _userService.DeleteUser(It.IsAny<int>());

            // Assert
            action.Should().BeTrue();
            _mockUserRepository.Verify(x => x.GetUser(It.IsAny<int>()), Times.Once);
            _mockUserRepository.Verify(x => x.DeleteUser(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnException_WhenUserNotFound()
        {
            //Arrange
            _mockUserRepository
                .Setup(x => x.GetUser(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<User>());

            // Act
            Func<Task> action = async () => await _userService.DeleteUser(It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockUserRepository.Verify(x => x.GetUser(It.IsAny<int>()), Times.Once);
            _mockUserRepository.Verify(x => x.DeleteUser(It.IsAny<int>()), Times.Never);
        }
    }
}
