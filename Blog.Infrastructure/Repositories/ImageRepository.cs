using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly BlogContext _context;

        public ImageRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Image>> GetImages(int userId)
        {
            var images = await _context
                .Images
                .Include(x => x.User)
                .Where(x => (userId == 0 || x.UserId == userId))
                .ToListAsync();

            return images;
        }

        public async Task<Image> GetImage(int id)
        {
            var image = await _context
                .Images
                .Include(x => x.User)
                .FirstOrDefaultAsync(u => u.ImageId == id);

            return image;
        }

        public async Task<Image> GetImage(string name)
        {
            var image = await _context
                .Images
                .Include(x => x.User)
                .FirstOrDefaultAsync(u => u.Name == name);

            return image;
        }

        public async Task InsertImage(Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteImage(int id)
        {
            var currentImage = await GetImage(id);
            _context.Images.Remove(currentImage);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
