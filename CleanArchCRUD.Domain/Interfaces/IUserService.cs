using CleanArchCRUD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchCRUD.Domain.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
        Task InsertUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
    }
}
