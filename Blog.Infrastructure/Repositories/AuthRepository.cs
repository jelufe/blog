using Blog.Domain.DTOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly BlogContext _context;

        public AuthRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByCredentials(AuthDto auth)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == auth.Email
                && u.Password == auth.Password
            );

            return user;
        }

        public async Task<bool> ChangePassword(User user, string newPassword)
        {
            user.Password = newPassword;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<User> ChangePassword(PasswordDto auth)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == auth.Email
                && u.Password == auth.OldPassword
            );

            return user;
        }
    }
}
