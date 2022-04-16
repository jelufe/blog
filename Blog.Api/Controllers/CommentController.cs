using AutoMapper;
using Blog.Api.Responses;
using Blog.Domain.DAOs;
using Blog.Domain.DTOs;
using Blog.Domain.Entities;
using Blog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : CustomControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            IEnumerable<CommentDao> comments = new List<CommentDao>();

            if (IsAdmin)
                comments = await _commentService.GetComments();
            else if (IsWriter)
                comments = await _commentService.GetComments(CurrentUserId);
            else if (IsReader)
                return Forbid();

            var response = new ApiResponse<IEnumerable<CommentDao>>(comments);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            var comment = await _commentService.GetComment(id);
            var response = new ApiResponse<CommentDao>(comment);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertComment([FromBody] CommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            await _commentService.InsertComment(comment);
            var response = new ApiResponse<Comment>(comment);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] Comment comment)
        {
            var result = await _commentService.UpdateComment(comment, IsAdmin, CurrentUserId);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var result = await _commentService.DeleteComment(id, IsAdmin, CurrentUserId);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
