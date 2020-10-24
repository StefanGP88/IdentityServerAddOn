using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend
{
    public class UserHandler : IUserHandler
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

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
