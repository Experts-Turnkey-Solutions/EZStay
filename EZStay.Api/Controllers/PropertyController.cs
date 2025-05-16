using Microsoft.AspNetCore.Mvc;
using EZStay.Api.Models.DTOs;
using EZStay.Api.Models.Domain;
using EZStay.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace EZStay.Api.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IMapper _mapper;

        public PropertyController(IPropertyService propertyService, IMapper mapper)
        {
            _propertyService = propertyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var properties = await _propertyService.GetAllPropertiesAsync();
            return Ok(properties);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null) return NotFound();
            return Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PropertyDto dto)
        {
            // Map PropertyDto to Property using AutoMapper
            var property = _mapper.Map<Property>(dto);
            property.Images = new List<Image>(); // Optional: fill based on dto if available

            var created = await _propertyService.CreatePropertyAsync(property);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PropertyDto dto)
        {
            // Map PropertyDto to Property using AutoMapper
            var updated = _mapper.Map<Property>(dto);
            updated.Id = id; // Ensure the correct ID is set for updating

            var result = await _propertyService.UpdatePropertyAsync(id, updated);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _propertyService.DeletePropertyAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
