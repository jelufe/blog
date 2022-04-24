using Blog.Domain.Entities;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface ISharingService
    {
        Task<bool> InsertSharing(Sharing sharing);
    }
}
