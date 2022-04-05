using Blog.Domain.Entities;

namespace Blog.Domain.DAOs
{
    public class PostDao
    {
        public PostDao(Post post)
        {
            PostId = post.PostId;
            Title = post.Title;
            Description = post.Description;
            User = post.User is null ? null : new UserDao(post.User);
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UserDao User { get; set; }
    }
}
