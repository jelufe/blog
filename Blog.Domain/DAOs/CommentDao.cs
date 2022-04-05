using Blog.Domain.Entities;

namespace Blog.Domain.DAOs
{
    public class CommentDao
    {
        public CommentDao(Comment comment)
        {
            CommentId = comment.CommentId;
            Message = comment.Message;
            User = comment.User is null ? null : new UserDao(comment.User);
        }

        public int CommentId { get; set; }
        public string Message { get; set; }
        public UserDao User { get; set; }
    }
}
