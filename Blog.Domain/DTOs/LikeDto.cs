using System;

namespace Blog.Domain.DTOs
{
    public class LikeDto
    {
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
