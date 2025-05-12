using System;

namespace EZStay.Api.Models.DTOs
{
    public class BookingDto
    {
        public Guid PropertyId { get; set; }
        public Guid UserId { get; set; } 
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
