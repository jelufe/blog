using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<NotificationDao>> GetNotifications(int userId = 0)
        {
            var notifications = await _notificationRepository.GetNotifications(userId);
            return notifications.Select(n => new NotificationDao(n)).ToList();
        }

        public async Task<NotificationDao> GetNotification(int id)
        {
            var notificationFound = await _notificationRepository.GetNotification(id);

            if (notificationFound == null)
                throw new Exception("Notification does not exist");

            return new NotificationDao(notificationFound);
        }

        public async Task<bool> InsertNotification(Notification notification)
        {
            notification.CreatedAt = DateTime.Now;

            return await _notificationRepository.InsertNotification(notification);
        }

        public async Task<bool> UpdateNotification(Notification notification, bool isAdmin, int currentUserId)
        {
            var notificationFound = await _notificationRepository.GetNotification(notification.NotificationId);

            if (notificationFound == null)
                throw new Exception("Notification does not exist");

            if (!isAdmin && notificationFound.User.UserId != currentUserId)
                throw new Exception("User does not have permission to perform this action");

            return await _notificationRepository.UpdateNotification(notification);
        }

        public async Task<bool> DeleteNotification(int id, bool isAdmin, int currentUserId)
        {
            var notificationFound = await _notificationRepository.GetNotification(id);

            if (notificationFound == null)
                throw new Exception("Notification does not exist");

            if (!isAdmin && notificationFound.User.UserId != currentUserId)
                throw new Exception("User does not have permission to perform this action");

            return await _notificationRepository.DeleteNotification(id);
        }
    }
}
