using Blog.Api.Responses;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DashboardController : CustomControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(
            IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            if (!IsAdmin && !IsWriter)
                return Forbid();

            var dashboardData = await _dashboardService.GetDashboardData();
            var response = new ApiResponse<Dashboard>(dashboardData);
            return Ok(response);
        }
    }
}
