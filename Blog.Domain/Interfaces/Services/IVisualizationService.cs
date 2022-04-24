using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface IVisualizationService
    {
        Task<VisualizationDao> GetVisualization(int postId, int userId);
        Task<VisualizationDao> GetVisualization(int postId, string sessionId);
        Task<bool> InsertVisualization(Visualization visualization);
        Task<bool> UpdateVisualization(Visualization visualization);
    }
}
