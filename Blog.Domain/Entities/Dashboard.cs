using System.Collections.Generic;

namespace Blog.Domain.Entities
{
    public class Dashboard
    {
        public IEnumerable<Chart> UsersMostCommentsInMonth { get; set; }
        public IEnumerable<Chart> PostsMostCommentsInMonth { get; set; }
        public IEnumerable<Chart> PostsMostViewsInMonth { get; set; }
        public IEnumerable<Chart> PostsMostSharesInMonth { get; set; }
        public IEnumerable<Chart> PostsMostLikesInMonth { get; set; }
    }
}
