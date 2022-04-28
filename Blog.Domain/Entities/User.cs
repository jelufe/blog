using System.Collections.Generic;

namespace Blog.Domain.Entities
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string GoogleId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
