using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationDao>> GetNotifications(int userId = 0);
        Task<NotificationDao> GetNotification(int id);
        Task<bool> InsertNotification(Notification notification);
        Task<bool> UpdateNotification(Notification notification, bool isAdmin, int currentUserId);
        Task<bool> DeleteNotification(int id, bool isAdmin, int currentUserId);
    }
}
