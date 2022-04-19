using Blog.Domain.Entities;

namespace Blog.Domain.DAOs
{
    public class ImageDao
    {
        public ImageDao(Image image)
        {
            ImageId = image.ImageId;
            Name = image.Name;
            Path = image.Path;
            User = image.User is null ? null : new UserDao(image.User);
        }

        public int ImageId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public virtual UserDao User { get; set; }
    }
}
