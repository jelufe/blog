using Blog.Domain.Entities;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Repositories
{
    public interface IDashboardRepository
    {
        Task<Dashboard> GetDashboardData();
    }
}
