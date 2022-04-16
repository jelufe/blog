using Blog.Domain.Entities;

namespace Blog.Domain.DAOs
{
    public class CommentDao
    {
        public CommentDao(Comment comment)
        {
            CommentId = comment.CommentId;
            Message = comment.Message;
            PostId = comment.PostId;
            User = comment.User is null ? null : new UserDao(comment.User);
            Post = comment.Post is null ? null : new PostDao(comment.Post);
        }

        public int CommentId { get; set; }
        public string Message { get; set; }
        public int PostId { get; set; }
        public UserDao User { get; set; }
        public PostDao Post { get; set; }
    }
}
