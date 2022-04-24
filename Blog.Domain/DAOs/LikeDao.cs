using Blog.Domain.Entities;
using System;

namespace Blog.Domain.DAOs
{
    public class LikeDao
    {
        public LikeDao(Like like)
        {
            LikeId = like.LikeId;
            CreatedAt = like.CreatedAt;
            UserId = like.UserId;
            PostId = like.PostId;
        }

        public int LikeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
