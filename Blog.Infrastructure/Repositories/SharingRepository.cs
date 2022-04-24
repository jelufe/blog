using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Repositories;
using Blog.Infrastructure.Data;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Repositories
{
    public class SharingRepository : ISharingRepository
    {
        private readonly BlogContext _context;

        public SharingRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertSharing(Sharing sharing)
        {
            _context.Shares.Add(sharing);
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
