namespace Blog.Domain.DTOs
{
    public class CommentDto
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
