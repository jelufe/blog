using Blog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotifications(int userId);
        Task<Notification> GetNotification(int id);
        Task<bool> InsertNotification(Notification notification);
        Task<bool> UpdateNotification(Notification notification);
        Task<bool> DeleteNotification(int id);
    }
}
