namespace Blog.Domain.Entities
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public virtual User User { get; set; }
        public virtual Image Image { get; set; }
    }
}
