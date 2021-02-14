using CleanArchCRUD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchCRUD.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
    }
}
