using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class VisualizationRepository : IVisualizationRepository
    {
        private readonly BlogContext _context;

        public VisualizationRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<Visualization> GetVisualization(int id)
        {
            var visualization = await _context
                .Visualizations
                .FirstOrDefaultAsync(u => u.VisualizationId == id);

            return visualization;
        }

        public async Task<Visualization> GetVisualizationByUser(int postId, int userId)
        {
            var visualization = await _context
                .Visualizations
                .Where(u => u.UserId == userId && u.PostId == postId)
                .FirstOrDefaultAsync();

            return visualization;
        }

        public async Task<Visualization> GetVisualizationBySession(int postId, string sessionId)
        {
            var visualization = await _context
                .Visualizations
                .Where(u => u.SessionId == sessionId && u.PostId == postId)
                .FirstOrDefaultAsync();

            return visualization;
        }

        public async Task<bool> InsertVisualization(Visualization visualization)
        {
            _context.Visualizations.Add(visualization);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> UpdateVisualization(Visualization visualization)
        {
            var visualizationPost = await GetVisualization(visualization.VisualizationId);
            visualizationPost.SessionId = visualization.SessionId;
            visualizationPost.UserId = visualization.UserId;
            visualizationPost.PostId = visualization.PostId;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
