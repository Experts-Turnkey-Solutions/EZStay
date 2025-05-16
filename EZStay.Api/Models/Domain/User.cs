namespace EZStay.Api.Models.Domain
{
    //public class User
    //{
    //    public Guid Id { get; set; }
    //    public string FullName { get; set; }
    //    public string Username { get; set; }
    //    public string Email { get; set; }
    //    public string PasswordHash { get; set; }
    //    public List<string> Roles { get; set; }
    //}

    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Make sure Roles is a List<string>
        public List<string> Roles { get; set; } = new List<string>();
    }
}
