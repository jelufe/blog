using Blog.Domain.DTOs;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> GetUserByCredentials(AuthDto auth, string secretKey, string issuer, string audience);
        Task<bool> ChangePassword(PasswordDto password);
    }
}
