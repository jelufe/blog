using Blog.Domain.Entities;
using System;

namespace Blog.Domain.DAOs
{
    public class NotificationDao
    {
        public NotificationDao(Notification notification)
        {
            NotificationId = notification.NotificationId;
            Title = notification.Title;
            Message = notification.Message;
            CreatedAt = notification.CreatedAt;
            User = notification.User is null ? null : new UserDao(notification.User);
            Receiver = notification.Receiver is null ? null : new UserDao(notification.Receiver);
        }

        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDao User { get; set; }
        public UserDao Receiver { get; set; }
    }
}
