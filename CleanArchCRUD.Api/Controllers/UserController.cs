using AutoMapper;
using CleanArchCRUD.Domain.DTOs;
using CleanArchCRUD.Domain.Entities;
using CleanArchCRUD.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.InsertUser(user);
            return Ok(user);
        }
    }
}
