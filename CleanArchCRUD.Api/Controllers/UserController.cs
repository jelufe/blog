using AutoMapper;
using CleanArchCRUD.Api.Responses;
using CleanArchCRUD.Domain.DTOs;
using CleanArchCRUD.Domain.Entities;
using CleanArchCRUD.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchCRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var response = new ApiResponse<IEnumerable<User>>(users);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            var response = new ApiResponse<User>(user);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.InsertUser(user);
            var response = new ApiResponse<User>(user);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            var result = await _userRepository.UpdateUser(user);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userRepository.DeleteUser(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
