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

        public async Task<bool> InsertImage(Image image)
        {
            _context.Images.Add(image);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteImage(int id)
        {
            var posts = await _context.Posts.Where(x => x.ImageId == id).ToListAsync();
            _context.Posts.RemoveRange(posts);

            var sharesbyPosts = await _context.Shares.Where(x => posts.Select(p => p.PostId).Contains(x.PostId)).ToListAsync();
            _context.Shares.RemoveRange(sharesbyPosts);

            var visualizationsbyPosts = await _context.Visualizations.Where(x => posts.Select(p => p.PostId).Contains(x.PostId)).ToListAsync();
            _context.Visualizations.RemoveRange(visualizationsbyPosts);

            var commentsbyPosts = await _context.Comments.Where(x => posts.Select(p => p.PostId).Contains(x.PostId)).ToListAsync();
            _context.Comments.RemoveRange(commentsbyPosts);

            var likesbyPosts = await _context.Likes.Where(x => posts.Select(p => p.PostId).Contains(x.PostId)).ToListAsync();
            _context.Likes.RemoveRange(likesbyPosts);

            var currentImage = await GetImage(id);
            _context.Images.Remove(currentImage);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
