using EZStay.Api.Models.Domain;

namespace EZStay.Api.Services
{
    public interface IUserService
    {
        Task<User?> GetUserWithRoleAsync(string email);
        Task<User?> GetUserByEmailOrUsernameAsync(string emailOrUsername);
        Task<bool> IsEmailOrUsernameTakenAsync(string email, string username);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(Guid id, User user);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
