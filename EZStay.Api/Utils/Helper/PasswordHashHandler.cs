namespace EZStay.Api.Utils.Helper
{
    public static class PasswordHashHandler
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }
    }
}
