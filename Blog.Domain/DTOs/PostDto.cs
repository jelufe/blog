namespace Blog.Domain.DTOs
{
    public class PostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
    }
}
