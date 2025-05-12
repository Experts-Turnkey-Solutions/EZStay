namespace EZStay.Api.Models.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } // For example: Guest, Admin, etc.
    }
}
