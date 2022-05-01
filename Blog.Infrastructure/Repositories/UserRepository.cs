using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;

        public UserRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            return user;
        }

        public async Task<bool> InsertUser(User user)
        {
            _context.Users.Add(user);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var currentUser = await GetUser(user.UserId);
            currentUser.Name = user.Name;
            currentUser.Type = user.Type;
            currentUser.Email = user.Email;
            currentUser.Password = user.Password;
            currentUser.GoogleId = user.GoogleId;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var shares = await _context.Shares.Where(x => x.UserId == id).ToListAsync();
            _context.Shares.RemoveRange(shares);

            var visualizations = await _context.Visualizations.Where(x => x.UserId == id).ToListAsync();
            _context.Visualizations.RemoveRange(visualizations);

            var comments = await _context.Comments.Where(x => x.UserId == id).ToListAsync();
            _context.Comments.RemoveRange(comments);

            var likes = await _context.Likes.Where(x => x.UserId == id).ToListAsync();
            _context.Likes.RemoveRange(likes);

            var notifications = await _context.Notifications.Where(x => x.UserId == id || x.ReceiverId == id).ToListAsync();
            _context.Notifications.RemoveRange(notifications);

            var posts = await _context.Posts.Where(x => x.UserId == id).ToListAsync();
            _context.Posts.RemoveRange(posts);

            var sharesbyPosts = await _context.Shares.Where(x => posts.Select(p => p.PostId).Contains(x.PostId)).ToListAsync();
            _context.Shares.RemoveRange(sharesbyPosts);

            var visualizationsbyPosts = await _context.Visualizations.Where(x => posts.Select(p => p.PostId).Contains(x.PostId)).ToListAsync();
            _context.Visualizations.RemoveRange(visualizationsbyPosts);

            var commentsbyPosts = await _context.Comments.Where(x => posts.Select(p => p.PostId).Contains(x.PostId)).ToListAsync();
            _context.Comments.RemoveRange(commentsbyPosts);

            var likesbyPosts = await _context.Likes.Where(x => posts.Select(p => p.PostId).Contains(x.PostId)).ToListAsync();
            _context.Likes.RemoveRange(likesbyPosts);

            var images = await _context.Images.Where(x => x.UserId == id).ToListAsync();
            _context.Images.RemoveRange(images);

            var postsByImages = await _context.Posts.Where(x => images.Select(i => i.ImageId).Contains(x.ImageId)).ToListAsync();
            _context.Posts.RemoveRange(posts);

            var currentUser = await GetUser(id);
            _context.Users.Remove(currentUser);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
