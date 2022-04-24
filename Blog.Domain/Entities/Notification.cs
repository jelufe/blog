using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Domain.Entities
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int? ReceiverId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual User Receiver { get; set; }
    }
}
