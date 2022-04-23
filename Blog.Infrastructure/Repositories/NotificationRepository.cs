using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly BlogContext _context;

        public NotificationRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetNotifications(int userId)
        {
            var notifications = await _context
                .Notifications
                .Include(x => x.User)
                .Include(x => x.Receiver)
                .Where(x => (userId == 0 || x.ReceiverId == userId))
                .ToListAsync();

            return notifications;
        }

        public async Task<Notification> GetNotification(int id)
        {
            var notification = await _context
                .Notifications
                .Include(x => x.User)
                .Include(x => x.Receiver)
                .FirstOrDefaultAsync(u => u.NotificationId == id);

            return notification;
        }

        public async Task<bool> InsertNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> UpdateNotification(Notification notification)
        {
            var currentNotification = await GetNotification(notification.NotificationId);
            currentNotification.Title = notification.Title;
            currentNotification.Message = notification.Message;
            currentNotification.UserId = notification.UserId;
            currentNotification.ReceiverId = notification.ReceiverId;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteNotification(int id)
        {
            var currentNotification = await GetNotification(id);
            _context.Notifications.Remove(currentNotification);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
