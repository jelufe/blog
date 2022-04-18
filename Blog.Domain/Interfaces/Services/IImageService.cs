using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface IImageService
    {
        Task InsertImage(IFormFile file);
    }
}
