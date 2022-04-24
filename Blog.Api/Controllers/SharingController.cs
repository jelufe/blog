using AutoMapper;
using Blog.Api.Responses;
using Blog.Domain.DTOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharingController : CustomControllerBase
    {
        private readonly ISharingService _sharingService;
        private readonly IMapper _mapper;

        public SharingController(
            ISharingService sharingService,
            IMapper mapper)
        {
            _sharingService = sharingService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> InsertSharing([FromBody] SharingDto sharingDto)
        {
            var sharing = _mapper.Map<Sharing>(sharingDto);
            await _sharingService.InsertSharing(sharing);
            var response = new ApiResponse<Sharing>(sharing);
            return Ok(response);
        }
    }
}
