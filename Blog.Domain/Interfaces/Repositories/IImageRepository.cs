using Blog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetImages(int userId);
        Task<Image> GetImage(int id);
        Task<Image> GetImage(string name);
        Task InsertImage(Image image);
        Task<bool> DeleteImage(int id);
    }
}
