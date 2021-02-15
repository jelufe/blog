using CleanArchCRUD.Domain.DTOs;
using CleanArchCRUD.Domain.Entities;
using System.Threading.Tasks;

namespace CleanArchCRUD.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetUserByCredentials(AuthDto auth);
        Task<bool> ChangePassword(User user, string newPassword);
        Task<User> ChangePassword(PasswordDto auth);
    }
}
