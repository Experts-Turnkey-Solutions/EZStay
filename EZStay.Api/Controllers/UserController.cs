using Microsoft.AspNetCore.Mvc;
using EZStay.Api.Models.Domain;
using EZStay.Api.Models.DTOs;
using EZStay.Api.Services;
using AutoMapper;

namespace EZStay.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            var userDtos = _mapper.Map<List<UserDto>>(users); // Map users to UserDto

            if (userDtos == null || !userDtos.Any()) return NotFound("No users found.");
            return Ok(userDtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();

            var userDto = _mapper.Map<UserDto>(user); // Map User to UserDto
            return Ok(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            // Map UserDto to User using AutoMapper
            var user = _mapper.Map<User>(dto);

            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserDto dto)
        {
            // Map UserDto to User using AutoMapper
            var updatedUser = _mapper.Map<User>(dto);
            updatedUser.Id = id; // Ensure the correct ID is set for updating

            var user = await _userService.UpdateUserAsync(id, updatedUser);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
