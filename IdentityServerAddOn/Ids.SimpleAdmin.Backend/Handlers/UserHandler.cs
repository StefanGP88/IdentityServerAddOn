using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class UserHandler<TContext> : IUserHandler where TContext : IdentityDbContext
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TContext _dbContext;
        public UserHandler(UserManager<IdentityUser> userManager, TContext context)
        {
            _userManager = userManager;
            _dbContext = context;
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
            await UpdateUserClaims(user, dto.Claims).ConfigureAwait(false);
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
            await UpdateUserClaims(user, dto.Claims).ConfigureAwait(false);
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
            var idUsers = await _userManager.Users
                        .OrderBy(x => x.UserName)
                        .Skip(page * pagesize)
                        .Take(pagesize)
                        .ToListAsync(cancel)
                        .ConfigureAwait(false);

            var userRoles = await Task.WhenAll(idUsers.ConvertAll(x => _userManager.GetRolesAsync(x))).ConfigureAwait(false);

            var userIds = idUsers.ConvertAll(c => c.Id);

            var idClaims = await _dbContext.UserClaims.Where(y => userIds.Contains(y.UserId)).ToListAsync(cancel).ConfigureAwait(false);

            var userResponseDtos = idUsers.ConvertAll(async x =>
            {
                var result = x.MapToDto();
                result.Roles = (List<string>)await _userManager.GetRolesAsync(x).ConfigureAwait(false);
                result.Claims = idClaims.Where(y => y.UserId == x.Id).Select(y => y.ToClaim()).ToList();
                return result;
            });

            var userResult = await Task.WhenAll(userResponseDtos).ConfigureAwait(false);

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

        private async Task UpdateUserClaims(IdentityUser user, Dictionary<string, string> claims)
        {
            var existingClaims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);

            var toAdd = new List<Claim>();
            var toRemove = existingClaims;
            var toUpdate = new List<Claim>();

            _ = claims.Select(x =>
             {
                 var exist = toRemove.FirstOrDefault(y => y.Value == x.Key);
                 if (exist != null)
                 {
                     toUpdate.Add(exist);
                     toRemove.Remove(exist);
                 }
                 else
                 {
                     toAdd.Add(new Claim(x.Key, x.Value));
                 }
                 return x;
             }).ToList();

            _ = toUpdate.ConvertAll(x =>
            {
                var claimType = claims[x.Value];
                if (claimType != x.Type)
                {
                    toRemove.Add(x);
                    toAdd.Add(new Claim(x.Value, claimType));
                }
                return x;
            });
            var removeClaimResult = await _userManager.RemoveClaimsAsync(user, toRemove).ConfigureAwait(false);
            if (!removeClaimResult.Succeeded)
            {
                throw new Exception("Could not remove claims");
            }
            var addClaimResult = await _userManager.AddClaimsAsync(user, toAdd).ConfigureAwait(false);
            if (!addClaimResult.Succeeded)
            {
                throw new Exception("Could not add claims");
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
