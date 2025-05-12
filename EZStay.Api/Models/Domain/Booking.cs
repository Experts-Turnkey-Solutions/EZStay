namespace EZStay.Api.Models.Domain
{
    public class Booking
    {
        public Guid Id { get; set; } 
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public bool IsCanceled { get; set; } = false;

        // Foreign Keys (now as Guid)
        public Guid UserId { get; set; }
        public Guid PropertyId { get; set; }
    }
}
