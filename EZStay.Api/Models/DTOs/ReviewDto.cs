namespace EZStay.Api.Models.DTOs
{
    public class ReviewDto
    {
        public int PropertyId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
