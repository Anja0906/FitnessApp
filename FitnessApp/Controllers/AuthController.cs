using FitnessApp.Application.Interfaces;
using FitnessApp.WebApi.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var user = await _userService.LoginUserAsync(loginRequest.Username, loginRequest.Password);
            if (user == null) return Unauthorized("Invalid credentials");

            var token = _tokenService.GenerateToken(user.Username, user.Id);
            return Ok(new { Token = token });
        }
    }
}
