namespace EZStay.Api.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Comma-separated roles
        public string Roles { get; set; }
    }
}
