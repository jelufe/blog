namespace CleanArchCRUD.Domain.DTOs
{
    public class PasswordDto
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
