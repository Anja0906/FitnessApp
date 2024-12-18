using AutoMapper;
using FitnessApp.Application.Interfaces;
using FitnessApp.Domain.Model;
using FitnessApp.WebApi.DTOs.Requests;
using FitnessApp.WebApi.DTOs.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var loggedInUserId = HttpContext.Items["UserId"] as string;
            if (loggedInUserId == null || loggedInUserId != id.ToString())
            {
                return Forbid();
            }
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound("User not found");
            var response = _mapper.Map<UserResponseDto>(user);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var response = _mapper.Map<List<UserResponseDto>>(users);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequestDto userRequestDto)
        {
            var user = _mapper.Map<User>(userRequestDto);
            var newUser = await _userService.AddUserAsync(user);
            var response = _mapper.Map<UserResponseDto>(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = response.Id }, response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateRequestDto userRequestDto)
        {
            var loggedInUserId = HttpContext.Items["UserId"] as string;
            if (loggedInUserId == null || loggedInUserId != userRequestDto.Id.ToString() || loggedInUserId != id.ToString())
            {
                return Forbid();
            }
            var user = _mapper.Map<User>(userRequestDto);
            var updatedUser = await _userService.UpdateUserAsync(user);
            var response = _mapper.Map<UserResponseDto>(updatedUser);
            return Ok(response);
        }
    }
}
