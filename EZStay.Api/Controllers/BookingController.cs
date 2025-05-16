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
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingDto dto)
        {
            // Map BookingDto to Booking using AutoMapper
            var booking = _mapper.Map<Booking>(dto);

            booking.IsCanceled = false; // Default value
            booking.UserId = dto.UserId; // User ID must be handled properly (e.g., from the context or token)

            var created = await _bookingService.CreateBookingAsync(booking);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BookingDto dto)
        {
            // Map BookingDto to Booking using AutoMapper
            var updated = _mapper.Map<Booking>(dto);
            updated.Id = id; // Ensure the correct ID is set for updating

            var result = await _bookingService.UpdateBookingAsync(id, updated);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var success = await _bookingService.CancelBookingAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
