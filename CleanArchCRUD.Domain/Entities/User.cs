#nullable disable

namespace CleanArchCRUD.Domain.Entities
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
