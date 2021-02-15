using CleanArchCRUD.Domain.DTOs;
using CleanArchCRUD.Domain.Entities;
using CleanArchCRUD.Domain.Interfaces;
using CleanArchCRUD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CleanArchCRUD.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CleanArchCRUDContext _context;

        public AuthRepository(CleanArchCRUDContext context)
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
