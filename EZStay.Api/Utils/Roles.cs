namespace EZStay.Api.Utils
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string Customer = "Customer";

        public static readonly List<string> AllRoles = new() { Admin, Manager, Customer };
    }
}
