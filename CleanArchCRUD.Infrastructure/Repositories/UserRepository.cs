using CleanArchCRUD.Domain.Entities;
using CleanArchCRUD.Domain.Interfaces;
using CleanArchCRUD.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
    }
}
