namespace EZStay.Api.Models.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
    }
}
