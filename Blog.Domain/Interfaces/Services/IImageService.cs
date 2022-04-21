using Blog.Domain.DAOs;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface IImageService
    {
        Task<IEnumerable<ImageDao>> GetImages(int userId = 0);
        Task<byte[]> GetImage(int id);
        Task InsertImage(IFormFile file, int currentUserId);
        Task<bool> DeleteImage(int id, bool isAdmin, int currentUserId);
    }
}
