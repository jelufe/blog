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
    public class NotificationServiceTests
    {
        private readonly Mock<INotificationRepository> _mockNotificationRepository;
        private readonly INotificationService _notificationService;

        private static Faker _faker = new Faker("pt_BR");

        public NotificationServiceTests()
        {
            _mockNotificationRepository = new Mock<INotificationRepository>();
            _notificationService = new NotificationService(_mockNotificationRepository.Object);
        }

        [Fact]
        public async Task GetNotifications_ShouldReturnNotificationList_WhenEverythingIsOk()
        {
            //Arrange
            _mockNotificationRepository
                .Setup(x => x.GetNotifications(It.IsAny<int>()))
                .ReturnsAsync(new List<Notification>());

            // Act
            var notifications = await _notificationService.GetNotifications(It.IsAny<int>());

            // Assert
            notifications.Should().BeOfType<List<NotificationDao>>();
            _mockNotificationRepository.Verify(x => x.GetNotifications(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetNotification_ShouldReturntNotification_WhenEverythingIsOk()
        {
            //Arrange
            _mockNotificationRepository
                .Setup(x => x.GetNotification(It.IsAny<int>()))
                .ReturnsAsync(new Notification());

            // Act
            var notification = await _notificationService.GetNotification(It.IsAny<int>());

            // Assert
            notification.Should().BeOfType<NotificationDao>();
            _mockNotificationRepository.Verify(x => x.GetNotification(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetNotification_ShouldReturnException_WhenNotificationNotFound()
        {
            //Arrange
            _mockNotificationRepository
                .Setup(x => x.GetNotification(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Notification>());

            // Act
            Func<Task> action = async () => await _notificationService.GetNotification(It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockNotificationRepository.Verify(x => x.GetNotification(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task InsertNotification_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockNotificationRepository
                .Setup(x => x.InsertNotification(It.IsAny<Notification>()))
                .ReturnsAsync(true);

            // Act
            var action = await _notificationService.InsertNotification(new Notification());

            // Assert
            action.Should().BeTrue();
            _mockNotificationRepository.Verify(x => x.InsertNotification(It.IsAny<Notification>()), Times.Once);
        }

        [Fact]
        public async Task UpdateNotification_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            var notification = new Notification()
            {
                NotificationId = _faker.Random.Int(1, 10)
            };

            _mockNotificationRepository
                .Setup(x => x.GetNotification(It.IsAny<int>()))
                .ReturnsAsync(new Notification());

            _mockNotificationRepository
                .Setup(x => x.UpdateNotification(It.IsAny<Notification>()))
                .ReturnsAsync(true);

            // Act
            var action = await _notificationService.UpdateNotification(notification, true, It.IsAny<int>());

            // Assert
            action.Should().BeTrue();
            _mockNotificationRepository.Verify(x => x.GetNotification(It.IsAny<int>()), Times.Once);
            _mockNotificationRepository.Verify(x => x.UpdateNotification(It.IsAny<Notification>()), Times.Once);
        }

        [Fact]
        public async Task UpdateNotification_ShouldReturnException_WhenNotificationNotFound()
        {
            //Arrange
            var notification = new Notification()
            {
                NotificationId = _faker.Random.Int(1, 10)
            };

            _mockNotificationRepository
                .Setup(x => x.GetNotification(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Notification>());

            // Act
            Func<Task> action = async () => await _notificationService.UpdateNotification(notification, true, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockNotificationRepository.Verify(x => x.GetNotification(It.IsAny<int>()), Times.Once);
            _mockNotificationRepository.Verify(x => x.UpdateNotification(It.IsAny<Notification>()), Times.Never);
        }

        [Fact]
        public async Task UpdateNotification_ShouldReturnException_WhenUserNotHavePermission()
        {
            //Arrange
            var notificationFound = new Notification()
            {
                User = new User()
                {
                    UserId = _faker.Random.Int(1, 10)
                }
            };

            var notification = new Notification()
            {
                NotificationId = _faker.Random.Int(1, 10)
            };

            _mockNotificationRepository
                .Setup(x => x.GetNotification(It.IsAny<int>()))
                .ReturnsAsync(notificationFound);

            // Act
            Func<Task> action = async () => await _notificationService.UpdateNotification(notification, false, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockNotificationRepository.Verify(x => x.GetNotification(It.IsAny<int>()), Times.Once);
            _mockNotificationRepository.Verify(x => x.UpdateNotification(It.IsAny<Notification>()), Times.Never);
        }

        [Fact]
        public async Task DeleteNotification_ShouldReturnTrue_WhenEverythingIsOk()
        {
            //Arrange
            _mockNotificationRepository
                .Setup(x => x.GetNotification(It.IsAny<int>()))
                .ReturnsAsync(new Notification());

            _mockNotificationRepository
                .Setup(x => x.DeleteNotification(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var action = await _notificationService.DeleteNotification(It.IsAny<int>(), true, It.IsAny<int>());

            // Assert
            action.Should().BeTrue();
            _mockNotificationRepository.Verify(x => x.GetNotification(It.IsAny<int>()), Times.Once);
            _mockNotificationRepository.Verify(x => x.DeleteNotification(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task DeleteNotification_ShouldReturnException_WhenNotificationNotFound()
        {
            //Arrange
            var notification = new Notification()
            {
                NotificationId = _faker.Random.Int(1, 10)
            };

            _mockNotificationRepository
                .Setup(x => x.GetNotification(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Notification>());

            // Act
            Func<Task> action = async () => await _notificationService.DeleteNotification(It.IsAny<int>(), true, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockNotificationRepository.Verify(x => x.GetNotification(It.IsAny<int>()), Times.Once);
            _mockNotificationRepository.Verify(x => x.DeleteNotification(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task DeleteNotification_ShouldReturnException_WhenUserNotHavePermission()
        {
            //Arrange
            var notificationFound = new Notification()
            {
                User = new User()
                {
                    UserId = _faker.Random.Int(1, 10)
                }
            };

            var notification = new Notification()
            {
                NotificationId = _faker.Random.Int(1, 10)
            };

            _mockNotificationRepository
                .Setup(x => x.GetNotification(It.IsAny<int>()))
                .ReturnsAsync(notificationFound);

            // Act
            Func<Task> action = async () => await _notificationService.DeleteNotification(It.IsAny<int>(), false, It.IsAny<int>());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _mockNotificationRepository.Verify(x => x.GetNotification(It.IsAny<int>()), Times.Once);
            _mockNotificationRepository.Verify(x => x.DeleteNotification(It.IsAny<int>()), Times.Never);
        }
    }
}
