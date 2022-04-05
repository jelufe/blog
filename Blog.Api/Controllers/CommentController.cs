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
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
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
            var comments = await _commentService.GetComments();
            var response = new ApiResponse<IEnumerable<CommentDao>>(comments);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _commentService.GetComment(id);
            var response = new ApiResponse<CommentDao>(comment);
            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> InsertComment(CommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            await _commentService.InsertComment(comment);
            var response = new ApiResponse<Comment>(comment);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateComment(Comment comment)
        {
            var result = await _commentService.UpdateComment(comment);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentService.DeleteComment(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
