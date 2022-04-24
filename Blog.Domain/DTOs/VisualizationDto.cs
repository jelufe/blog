using System;

namespace Blog.Domain.DTOs
{
    public class VisualizationDto
    {
        public string SessionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }
        public int PostId { get; set; }
    }
}
