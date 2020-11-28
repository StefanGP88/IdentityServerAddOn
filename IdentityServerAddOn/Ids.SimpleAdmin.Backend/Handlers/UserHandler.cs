using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
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
            return user.MapToDto();
        }

        public async Task<ListDto<UserResponseDto>> ReadAllUsers(int page, int pagesize, CancellationToken cancel)
        {
            return new ListDto<UserResponseDto>
            {
                Page = page,
                PageSize = pagesize,
                TotalItems = await _userManager
                        .Users.CountAsync(cancel)
                        .ConfigureAwait(false),
                Items = await _userManager
                        .Users
                        .Skip(page * pagesize)
                        .Take(pagesize)
                        .Select(x => x.MapToDto())
                        .ToListAsync(cancel)
                        .ConfigureAwait(false)
            };
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
