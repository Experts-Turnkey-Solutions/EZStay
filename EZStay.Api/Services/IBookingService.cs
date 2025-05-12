using EZStay.Api.Models.Domain;

namespace EZStay.Api.Services
{
    public interface IBookingService
    {
        Task<Booking?> GetBookingByIdAsync(Guid id);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<Booking?> UpdateBookingAsync(Guid id, Booking booking);
        Task<bool> CancelBookingAsync(Guid id);
    }
}
