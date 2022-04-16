using AutoMapper;
using Blog.Api.Responses;
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
    public class UserController : CustomControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            if (!IsAdmin)
                return Forbid();

            var users = await _userService.GetUsers();
            var response = new ApiResponse<IEnumerable<User>>(users);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!IsAdmin && CurrentUserId != id)
                return Forbid();

            var user = await _userService.GetUser(id);
            var response = new ApiResponse<User>(user);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser([FromBody] UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userService.InsertUser(user);
            var response = new ApiResponse<User>(user);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (!IsAdmin && CurrentUserId != user.UserId)
                return Forbid();

            var result = await _userService.UpdateUser(user);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!IsAdmin)
                return Forbid();

            var result = await _userService.DeleteUser(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
