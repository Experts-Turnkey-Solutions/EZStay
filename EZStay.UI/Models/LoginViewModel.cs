namespace EZStay.UI.Models
{
    public class LoginViewModel
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "user";
    }
}