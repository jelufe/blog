using Blog.Domain.Entities;
using System;

namespace Blog.Domain.DAOs
{
    public class CommentDao
    {
        public CommentDao(Comment comment)
        {
            CommentId = comment.CommentId;
            Message = comment.Message;
            CreatedAt = comment.CreatedAt;
            PostId = comment.PostId;
            User = comment.User is null ? null : new UserDao(comment.User);
            Post = comment.Post is null ? null : new PostDao(comment.Post);
        }

        public int CommentId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }
        public UserDao User { get; set; }
        public PostDao Post { get; set; }
    }
}
