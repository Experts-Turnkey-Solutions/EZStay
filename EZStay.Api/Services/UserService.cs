using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EZStay.Api.Models.Domain;
using EZStay.Api.Repositories;
using EZStay.Api.Utils.Core;

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
            // Check if the email or username is already registered
            var existingUser = await _repository.Query()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == user.Email.ToLower() || u.Username.ToLower() == user.Username.ToLower());

            if (existingUser != null)
            {
                // Split the existing roles string into a list of roles
                var existingRoles = existingUser.Roles.Split(',').ToList();

                // Check if the new role is already assigned to the user
                if (existingRoles.Contains(user.Roles))
                {
                    // If the role already exists, throw an exception to prevent registration
                    throw new InvalidOperationException("This role has already been assigned to this user.");
                }

                // Add the new role to the existing roles list
                existingRoles.Add(user.Roles);

                // Join the roles back into a comma-separated string
                existingUser.Roles = string.Join(",", existingRoles);

                // Update the existing user
                await _repository.UpdateAsync(existingUser.Id, existingUser);

                return existingUser; // Return updated user
            }

            // If user is not found, create a new user with the selected role
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
