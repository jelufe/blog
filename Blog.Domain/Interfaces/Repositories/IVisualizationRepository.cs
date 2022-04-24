using Blog.Domain.Entities;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface IVisualizationRepository
    {
        Task<Visualization> GetVisualization(int id);
        Task<Visualization> GetVisualizationByUser(int postId, int userId);
        Task<Visualization> GetVisualizationBySession(int postId, string sessionId);
        Task<bool> InsertVisualization(Visualization visualization);
        Task<bool> UpdateVisualization(Visualization visualization);
    }
}
