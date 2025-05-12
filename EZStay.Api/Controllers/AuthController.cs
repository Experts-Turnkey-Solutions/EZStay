using Microsoft.AspNetCore.Mvc;
using EZStay.Api.Models.DTOs;
using EZStay.Api.Models.Domain;
using EZStay.Api.Services;
using AutoMapper;

namespace EZStay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // Map RegisterDto to User using AutoMapper
            var user = _mapper.Map<User>(dto);

            user.Role = new Role { Name = "Guest" };

            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var users = await _userService.GetAllUsersAsync();
            var user = users.FirstOrDefault(u => u.Email == dto.Email && u.PasswordHash == dto.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            return Ok(user);
        }
    }
}
