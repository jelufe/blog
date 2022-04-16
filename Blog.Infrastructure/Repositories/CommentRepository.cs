using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext _context;

        public CommentRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetComments(int userId)
        {
            var comments = await _context
                .Comments
                .Include(x => x.User)
                .Include(x => x.Post)
                .Where(x => (userId == 0 || x.UserId == userId))
                .ToListAsync();

            return comments;
        }

        public async Task<Comment> GetComment(int id)
        {
            var comments = await _context
                .Comments
                .Include(x => x.User)
                .FirstOrDefaultAsync(u => u.CommentId == id);

            return comments;
        }

        public async Task InsertComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateComment(Comment comment)
        {
            var currentComment = await GetComment(comment.CommentId);
            currentComment.Message = comment.Message;
            currentComment.UserId = comment.UserId;
            currentComment.PostId = comment.PostId;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteComment(int id)
        {
            var currentComment = await GetComment(id);
            _context.Comments.Remove(currentComment);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
