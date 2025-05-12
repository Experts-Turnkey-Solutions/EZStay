namespace EZStay.Api.Models.Domain
{
    public class Property
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public List<Image> Images { get; set; }
    }
}
