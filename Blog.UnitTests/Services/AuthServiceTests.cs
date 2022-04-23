using Blog.Domain.DTOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using Blog.Domain.Services;
using Bogus;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Blog.UnitTests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _mockAuthRepository;
        private readonly IAuthService _authService;

        private static Faker _faker = new Faker("pt_BR");

        public AuthServiceTests()
        {
            _mockAuthRepository = new Mock<IAuthRepository>();
            _authService = new AuthService(_mockAuthRepository.Object);
        }

        [Fact]
        public async Task GetUserByCredentials_ShouldReturnToken_WhenEverythingIsOk()
        {
            //Arrange
            var user = new User()
            {
                UserId = _faker.Random.Int(1, 9),
                Name = _faker.Random.AlphaNumeric(12),
                Email = _faker.Person.Email,
                Type = _faker.Random.AlphaNumeric(5)
            };

            _mockAuthRepository
                .Setup(x => x.GetUserByCredentials(It.IsAny<AuthDto>()))
                .ReturnsAsync(user);

            // Act
            var token = await _authService.GetUserByCredentials(
                It.IsAny<AuthDto>(),
                _faker.Random.AlphaNumeric(37),
                _faker.Random.AlphaNumeric(12),
                _faker.Random.AlphaNumeric(12));

            // Assert
            token.Should().BeOfType<string>();
            _mockAuthRepository.Verify(x => x.GetUserByCredentials(It.IsAny<AuthDto>()), Times.Once);
        }

        [Fact]
        public async Task GetUserByCredentials_ShouldReturnException_WhenUserNotFound()
        {
            //Arrange
            _mockAuthRepository
                .Setup(x => x.GetUserByCredentials(It.IsAny<AuthDto>()))
                .ReturnsAsync(It.IsAny<User>());

            // Act
            Func<Task> action = async () => await _authService.GetUserByCredentials(
                It.IsAny<AuthDto>(),
                _faker.Random.AlphaNumeric(37),
                _faker.Random.AlphaNumeric(12),
                _faker.Random.AlphaNumeric(12));

            //Assert
            action.Should().ThrowAsync<Exception>();
            _mockAuthRepository.Verify(x => x.GetUserByCredentials(It.IsAny<AuthDto>()), Times.Once);
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            var passwordDto = new PasswordDto()
            {
                Email = _faker.Person.Email,
                OldPassword = _faker.Internet.Password(12),
                NewPassword = _faker.Internet.Password(10)
            };

            _mockAuthRepository
                .Setup(x => x.ChangePassword(It.IsAny<PasswordDto>()))
                .ReturnsAsync(new User());

            _mockAuthRepository
                .Setup(x => x.ChangePassword(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var action = await _authService.ChangePassword(passwordDto);

            // Assert
            action.Should().BeTrue();
            _mockAuthRepository.Verify(x => x.ChangePassword(It.IsAny<PasswordDto>()), Times.Once);
            _mockAuthRepository.Verify(x => x.ChangePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnException_WhenIncorrectEmailOrOldPassword()
        {
            //Arrange
            _mockAuthRepository
                .Setup(x => x.ChangePassword(It.IsAny<PasswordDto>()))
                .ReturnsAsync(It.IsAny<User>());

            // Act
            Func<Task> action = async () => await _authService.ChangePassword(It.IsAny<PasswordDto>());

            //Assert
            action.Should().ThrowAsync<Exception>();
            _mockAuthRepository.Verify(x => x.ChangePassword(It.IsAny<PasswordDto>()), Times.Once);
            _mockAuthRepository.Verify(x => x.ChangePassword(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }
    }
}
