using Blog.Domain.Entities;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface ISharingRepository
    {
        Task<bool> InsertSharing(Sharing sharing);
    }
}
