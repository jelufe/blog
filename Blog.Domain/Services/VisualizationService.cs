using Blog.Domain.DAOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Blog.Domain.Services
{
    public class VisualizationService : IVisualizationService
    {
        private readonly IVisualizationRepository _visualizationRepository;

        public VisualizationService(IVisualizationRepository visualizationRepository)
        {
            _visualizationRepository = visualizationRepository;
        }

        public async Task<VisualizationDao> GetVisualization(int postId, int userId)
        {
            var visualizationFound = await _visualizationRepository.GetVisualizationByUser(postId, userId);

            if (visualizationFound == null)
                throw new Exception("Visualization does not exist");

            return new VisualizationDao(visualizationFound);
        }

        public async Task<VisualizationDao> GetVisualization(int postId, string sessionId)
        {
            var visualizationFound = await _visualizationRepository.GetVisualizationBySession(postId, sessionId);

            if (visualizationFound == null)
                throw new Exception("Visualization does not exist");

            return new VisualizationDao(visualizationFound);
        }

        public async Task<bool> InsertVisualization(Visualization visualization)
        {
            visualization.CreatedAt = DateTime.Now;

            return await _visualizationRepository.InsertVisualization(visualization);
        }

        public async Task<bool> UpdateVisualization(Visualization visualization)
        {
            var visualizationFound = await _visualizationRepository.GetVisualization(visualization.VisualizationId);

            if (visualizationFound == null)
                throw new Exception("Visualization does not exist");

            return await _visualizationRepository.UpdateVisualization(visualization);
        }
    }
}
