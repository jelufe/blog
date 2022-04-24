using Blog.Domain.Entities;
using System.Threading.Tasks;

namespace Blog.Domain.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<Dashboard> GetDashboardData();
    }
}
