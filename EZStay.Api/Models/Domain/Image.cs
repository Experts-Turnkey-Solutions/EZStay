namespace EZStay.Api.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; }

        public Guid PropertyId { get; set; }              // Foreign Key
        public Property Property { get; set; }            // Navigation Property
    }
}
