using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly BlogContext _context;

        public LikeRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<Like> GetLike(int postId, int userId)
        {
            var visualization = await _context
                .Likes
                .Where(u => u.UserId == userId && u.PostId == postId)
                .FirstOrDefaultAsync();

            return visualization;
        }

        public async Task<bool> InsertLike(Like like)
        {
            _context.Likes.Add(like);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteLike(int postId, int userId)
        {
            var currentLike = await GetLike(postId, userId);
            _context.Likes.Remove(currentLike);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
