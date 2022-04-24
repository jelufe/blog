using Blog.Domain.Entities;
using System;

namespace Blog.Domain.DAOs
{
    public class VisualizationDao
    {
        public VisualizationDao()
        { }

        public VisualizationDao(Visualization visualization)
        {
            VisualizationId = visualization.VisualizationId;
            SessionId = visualization.SessionId;
            CreatedAt = visualization.CreatedAt;
            UserId = visualization.UserId;
            PostId = visualization.PostId;
        }

        public int VisualizationId { get; set; }
        public string SessionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }
        public int PostId { get; set; }
    }
}
