using Blog.Domain.Entities;

namespace Blog.Domain.DAOs
{
    public class UserDao
    {
        public UserDao(User user)
        {
            UserId = user.UserId;
            Name = user.Name;
        }

        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
