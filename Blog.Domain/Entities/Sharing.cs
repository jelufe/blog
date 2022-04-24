using System;

namespace Blog.Domain.Entities
{
    public class Sharing
    {
        public int SharingId { get; set; }
        public string SessionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }
        public int PostId { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
