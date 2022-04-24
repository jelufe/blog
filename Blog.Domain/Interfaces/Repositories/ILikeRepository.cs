using Blog.Domain.Entities;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface ILikeRepository
    {
        Task<Like> GetLike(int postId, int userId);
        Task<bool> InsertLike(Like like);
        Task<bool> DeleteLike(int postId, int userId);
    }
}
