using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class UserHandler : IUserHandler
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        //TODO: Add asign role to user functionality
        public async Task CreateUser(CreateUserRequestDto dto)
        {
            await IsEmailAvailable(dto.Email).ConfigureAwait(false);
            await IsUsernameAvailable(dto.Username).ConfigureAwait(false);
            await IsValidPassword(dto.Password).ConfigureAwait(false);

            var user = dto.MapToModel(_userManager);
            var result = await _userManager.CreateAsync(user, dto.Password).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                throw new Exception("Not created");
            }
            await UpdateUserRole(user, dto.Roles).ConfigureAwait(false);
        }

        public async Task UpdateUser(UpdateUserRequestDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.Userid).ConfigureAwait(false);
            if (user == null) throw new Exception("User not found");
            if (user.Email != dto.Email)
            {
                await IsEmailAvailable(dto.Email).ConfigureAwait(false);
            }
            if (user.UserName != dto.Username)
            {
                await IsUsernameAvailable(dto.Username).ConfigureAwait(false);
            }
            user = user.UpdateModel(_userManager, dto);
            var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                throw new Exception("Not updated");
            }
            await UpdateUserRole(user, dto.Roles).ConfigureAwait(false);       
        }

        public async Task DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null) throw new Exception("User not found");
            await _userManager.DeleteAsync(user).ConfigureAwait(false);
        }

        public async Task<UserResponseDto> ReadUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null) throw new Exception("User not found");
            return await user.MapToDtoAsync(_userManager).ConfigureAwait(false);
        }

        public async Task<ListDto<UserResponseDto>> ReadAllUsers(int page, int pagesize, CancellationToken cancel)
        {
            var userQuery = _userManager.Users
                        .OrderBy(x => x.UserName)
                        .Skip(page * pagesize)
                        .Take(pagesize)
                        .Select(x => x.MapToDtoAsync(_userManager))
                        .ToList();

            var userResult = await Task.WhenAll(userQuery).ConfigureAwait(false);

            return new ListDto<UserResponseDto>
            {
                Page = page,
                PageSize = pagesize,
                TotalItems = userResult.Length,
                Items = userResult.ToList()
            };
        }

        private async Task UpdateUserRole(IdentityUser user, List<string> roles)
        {
            var currentRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var toRemove = currentRoles.Where(x => !roles.Contains(x)).ToList();
            var toAdd = roles.Where(x => !currentRoles.Contains(x)).ToList();

            var roleRemoveResult = await _userManager.RemoveFromRolesAsync(user, toRemove).ConfigureAwait(false);
            if (!roleRemoveResult.Succeeded)
            {
                throw new Exception("Roles was not removed from user");
            }
            var roleAddResult = await _userManager.AddToRolesAsync(user, toAdd).ConfigureAwait(false);
            if (!roleAddResult.Succeeded)
            {
                throw new Exception("Roles was not added to user");
            }
        }

        private async Task IsEmailAvailable(string email)
        {
            var user = await _userManager.FindByEmailAsync(email).ConfigureAwait(false);
            if (user != null) throw new Exception("Email not available");
        }

        private async Task IsUsernameAvailable(string username)
        {
            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);
            if (user != null) throw new Exception("Username not available");
        }

        private async Task IsValidPassword(string pw)
        {
            foreach (var item in _userManager.PasswordValidators)
            {
                var result = await item.ValidateAsync(_userManager, null, pw).ConfigureAwait(false);
                if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
            }
        }
    }
}
