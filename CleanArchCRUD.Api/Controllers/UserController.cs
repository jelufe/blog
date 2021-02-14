using CleanArchCRUD.Domain.Interfaces;
using CleanArchCRUD.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchCRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(users);
        }
    }
}
