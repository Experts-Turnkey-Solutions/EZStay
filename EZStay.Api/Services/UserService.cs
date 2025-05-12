using EZStay.Api.Models.Domain;
using EZStay.Api.Repositories;

namespace EZStay.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> CreateUserAsync(User user) =>
            await _repository.AddAsync(user);

        public async Task<bool> DeleteUserAsync(Guid id) =>
            await _repository.DeleteAsync(id);

        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _repository.GetAllAsync();

        public async Task<User?> GetUserByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<User?> UpdateUserAsync(Guid id, User user) =>
            await _repository.UpdateAsync(id, user);
    }
}
