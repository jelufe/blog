using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface ILikeService
    {
        Task<LikeDao> GetLike(int postId, int userId);
        Task<bool> InsertLike(Like like, int userId);
        Task<bool> DeleteLike(int postId, int userId);
    }
}
