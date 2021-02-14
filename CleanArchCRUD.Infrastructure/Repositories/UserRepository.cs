using CleanArchCRUD.Domain.Entities;
using CleanArchCRUD.Domain.Interfaces;
using CleanArchCRUD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchCRUD.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CleanArchCRUDContext _context;

        public UserRepository(CleanArchCRUDContext context)
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

        public async Task InsertUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateUser(User user)
        {
            var currentUser = await GetUser(user.UserId);
            currentUser.Name = user.Name;
            currentUser.Email = user.Email;
            currentUser.Password = user.Password;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var currentUser = await GetUser(id);
            _context.Users.Remove(currentUser);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
