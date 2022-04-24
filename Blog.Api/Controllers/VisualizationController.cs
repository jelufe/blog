using AutoMapper;
using Blog.Api.Responses;
using Blog.Domain.DTOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisualizationController : CustomControllerBase
    {
        private readonly IVisualizationService _visualizationService;
        private readonly IMapper _mapper;

        public VisualizationController(
            IVisualizationService visualizationService,
            IMapper mapper)
        {
            _visualizationService = visualizationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> InsertVisualization([FromBody] VisualizationDto visualizationDto)
        {
            var visualization = _mapper.Map<Visualization>(visualizationDto);
            await _visualizationService.InsertVisualization(visualization);
            var response = new ApiResponse<Visualization>(visualization);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateVisualization([FromBody] Visualization visualization)
        {
            var result = await _visualizationService.UpdateVisualization(visualization);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
