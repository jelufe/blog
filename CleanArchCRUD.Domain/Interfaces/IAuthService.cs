using CleanArchCRUD.Domain.DTOs;
using System.Threading.Tasks;

namespace CleanArchCRUD.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<string> GetUserByCredentials(AuthDto auth, string secretKey, string issuer, string audience);
        Task<bool> ChangePassword(PasswordDto password);
    }
}
