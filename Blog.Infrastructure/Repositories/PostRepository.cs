using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPosts(int userId)
        {
            var posts = await _context
                .Posts
                .Include(x => x.User)
                .Include(x => x.Image)
                .Where(x => (userId == 0 || x.UserId == userId))
                .ToListAsync();

            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await _context
                .Posts
                .Include(x => x.User)
                .Include(x => x.Image)
                .FirstOrDefaultAsync(u => u.PostId == id);

            return post;
        }

        public async Task<IEnumerable<Comment>> GetComments(int id)
        {
            var posts = await _context
                .Comments
                .Include(x => x.User)
                .Where(x => x.PostId == id)
                .ToListAsync();

            return posts;
        }

        public async Task<bool> InsertPost(Post post)
        {
            _context.Posts.Add(post);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var currentPost = await GetPost(post.PostId);
            currentPost.Title = post.Title;
            currentPost.Description = post.Description;
            currentPost.UserId = post.UserId;
            currentPost.ImageId = post.ImageId;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeletePost(int id)
        {
            var shares = await _context.Shares.Where(x => x.PostId == id).ToListAsync();
            _context.Shares.RemoveRange(shares);

            var visualizations = await _context.Visualizations.Where(x => x.PostId == id).ToListAsync();
            _context.Visualizations.RemoveRange(visualizations);

            var comments = await _context.Comments.Where(x => x.PostId == id).ToListAsync();
            _context.Comments.RemoveRange(comments);

            var likes = await _context.Likes.Where(x => x.PostId == id).ToListAsync();
            _context.Likes.RemoveRange(likes);

            var currentPost = await GetPost(id);
            _context.Posts.Remove(currentPost);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
