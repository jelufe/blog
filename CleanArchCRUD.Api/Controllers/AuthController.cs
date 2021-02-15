using CleanArchCRUD.Api.Responses;
using CleanArchCRUD.Domain.DTOs;
using CleanArchCRUD.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CleanArchCRUD.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Authentication(AuthDto auth)
        {
            var token = await _authService.GetUserByCredentials(
                auth,
                _configuration["Authentication:SecretKey"],
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"]
            );
            var response = new ApiResponse<String>(token);
            return Ok(response);
        }

        [HttpPatch]
        public async Task<IActionResult> ChangePassword(PasswordDto password)
        {
            var result = await _authService.ChangePassword(password);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
