namespace Blog.Domain.DTOs
{
    public class NotificationDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public int? ReceiverId { get; set; }
    }
}
