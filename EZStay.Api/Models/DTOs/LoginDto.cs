namespace EZStay.Api.Models.DTOs
{
    public class LoginDto
    {
        public string EmailOrUsername { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

    }
}
