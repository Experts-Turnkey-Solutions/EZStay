namespace EZStay.Api.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }  
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
    }
}
