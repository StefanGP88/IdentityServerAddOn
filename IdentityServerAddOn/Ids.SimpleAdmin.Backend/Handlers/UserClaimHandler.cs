using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class UserClaimHandler : IUserClaimHandler
    {
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly IUserClaimStore<IdentityUser> _userClaimStore;
        public UserClaimHandler(UserManager<IdentityUser> userManager/*, IUserClaimStore<IdentityUser> userClaimStore*/)
        {
            _userManager = userManager;
           // _userClaimStore = userClaimStore;
        }
        public async Task<UserClaimResponseDto> CreateRoleClaim(CreateUserClaimRequestDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId).ConfigureAwait(false);
            if (user == null) throw new Exception("User not found");

            var claim = dto.MapToModel();
            var result = await _userManager.AddClaimAsync(user, claim).ConfigureAwait(false);
            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
            return claim.MapToDto(user);
        }

        public async Task<ListDto<UserClaimResponseDto>> GetUserClaims(string userId, int page, int pageSize)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null) throw new Exception("User not found");

            var userClaims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);
            return new ListDto<UserClaimResponseDto>
            {
                Page = page,
                PageSize = pageSize,
                Total = userClaims.Count,
                Items = userClaims.Select(x => x.MapToDto(user)).ToList()
            };
        }

        public async Task RemoveRoleClaim(RemoveUserClaimRequestDto dto, CancellationToken cancel)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId).ConfigureAwait(false);
            if (user == null) throw new Exception("User not found");

            //var userClaims = await _userClaimStore.GetClaimsAsync(user, cancel).ConfigureAwait(false);
            var userClaims = await _userManager.GetClaimsAsync(user).ConfigureAwait(false);
            var claim = userClaims.FirstOrDefault(cancel => cancel.Type == dto.Type);
            if (claim == null) throw new Exception("Claim not found");

            var result = await _userManager.RemoveClaimAsync(user, claim).ConfigureAwait(false);
            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
        }
    }
}
