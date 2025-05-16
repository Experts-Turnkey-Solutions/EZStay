using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User> CreateUserAsync(User user)
        {
            var existingUser = await _repository.Query()
                .FirstOrDefaultAsync(u =>
                    u.Email.ToLower() == user.Email.ToLower() ||
                    u.Username.ToLower() == user.Username.ToLower());

            if (existingUser != null)
            {
                // Normalize new roles to lower case
                var newRoles = user.Roles.Select(r => r.ToLower()).ToList();

                // Add roles only if they do not exist already
                var rolesToAdd = newRoles.Except(existingUser.Roles.Select(r => r.ToLower())).ToList();

                if (!rolesToAdd.Any())
                    throw new InvalidOperationException("This role has already been assigned to this user.");

                existingUser.Roles.AddRange(rolesToAdd);

                await _repository.UpdateAsync(existingUser.Id, existingUser);

                return existingUser;
            }

            // Normalize roles before creation
            user.Roles = user.Roles.Select(r => r.ToLower()).ToList();

            return await _repository.AddAsync(user);
        }

        public async Task<bool> DeleteUserAsync(Guid id) =>
            await _repository.DeleteAsync(id);

        public async Task<IEnumerable<User>> GetAllUsersAsync() =>
            await _repository.GetAllAsync();

        public async Task<User?> GetUserByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<User?> GetUserWithRoleAsync(string email)
        {
            return await _repository.Query()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetUserByEmailOrUsernameAsync(string emailOrUsername)
        {
            return await _repository.Query()
                .FirstOrDefaultAsync(u =>
                    u.Email.ToLower() == emailOrUsername.ToLower() ||
                    u.Username.ToLower() == emailOrUsername.ToLower());
        }

        public async Task<bool> IsEmailOrUsernameTakenAsync(string email, string username)
        {
            return await _repository.Query()
                .AnyAsync(u =>
                    u.Email.ToLower() == email.ToLower() ||
                    u.Username.ToLower() == username.ToLower());
        }

        public async Task<User?> UpdateUserAsync(Guid id, User user) =>
            await _repository.UpdateAsync(id, user);
    }
}
