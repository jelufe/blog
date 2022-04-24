using System;

namespace Blog.Domain.Entities
{
    public class Like
    {
        public int LikeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
