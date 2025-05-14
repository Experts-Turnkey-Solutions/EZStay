using Microsoft.AspNetCore.Mvc;
using EZStay.Api.Models.DTOs;
using EZStay.Api.Models.Domain;
using EZStay.Api.Services;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EZStay.Api.Utils.Core;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace EZStay.Api.Controllers // namespace EZStay.Api.Controllers.v1 namespace for clarity (optional)
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (dto.Role.ToLower() == "admin")
                return BadRequest("Invalid type.");

            var existingUser = await _userService.GetUserByEmailOrUsernameAsync(dto.Email);

            if (existingUser != null)
            {
                var rolesList = existingUser.Roles.Split(',').Select(r => r.Trim().ToLower()).ToList();

                if (rolesList.Contains(dto.Role.ToLower()))
                    return BadRequest("This role has already been assigned to this user.");

                rolesList.Add(dto.Role);
                existingUser.Roles = string.Join(",", rolesList);
                await _userService.UpdateUserAsync(existingUser.Id, existingUser);
                return Ok(existingUser);
            }

            var newUser = _mapper.Map<User>(dto);
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            newUser.Roles = dto.Role;

            var createdUser = await _userService.CreateUserAsync(newUser);
            return CreatedAtAction(nameof(Register), new { id = createdUser.Id }, createdUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.GetUserByEmailOrUsernameAsync(dto.EmailOrUsername);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var rolesList = user.Roles.Split(',').Select(r => r.Trim().ToLower()).ToList();
            if (!rolesList.Contains(dto.Role.ToLower()))
                return Unauthorized("You are not authorized for this role.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, dto.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { User = user, Token = tokenString });
        }
    }
}
