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
    [Authorize]
    [ApiController]
    public class LikeController : CustomControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly IMapper _mapper;

        public LikeController(
            ILikeService likeService,
            IMapper mapper)
        {
            _likeService = likeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> InsertLike([FromBody] LikeDto likeDto)
        {
            var like = _mapper.Map<Like>(likeDto);
            await _likeService.InsertLike(like, CurrentUserId);
            var response = new ApiResponse<Like>(like);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteLike([FromQuery] int postId)
        {
            var result = await _likeService.DeleteLike(postId, CurrentUserId);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
