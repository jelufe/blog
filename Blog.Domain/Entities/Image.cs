namespace Blog.Domain.Entities
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
