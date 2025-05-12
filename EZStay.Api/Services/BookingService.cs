using EZStay.Api.Models.Domain;
using EZStay.Api.Repositories;

namespace EZStay.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _repository;

        public BookingService(IRepository<Booking> repository)
        {
            _repository = repository;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking) =>
            await _repository.AddAsync(booking);

        public async Task<bool> CancelBookingAsync(Guid id) =>
            await _repository.DeleteAsync(id); 

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync() =>
            await _repository.GetAllAsync();

        public async Task<Booking?> GetBookingByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<Booking?> UpdateBookingAsync(Guid id, Booking booking) =>
            await _repository.UpdateAsync(id, booking);
    }
}
