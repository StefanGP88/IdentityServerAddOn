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
    public class RoleClaimHandler : IRoleClaimHandler
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IRoleClaimStore<IdentityRole> _roleClaimStore;

        public RoleClaimHandler(RoleManager<IdentityRole> roleManager/*, IRoleClaimStore<IdentityRole> roleClaimStore*/)
        {
            _roleManager = roleManager;
            //_roleClaimStore = roleClaimStore;
            //_roleManager.cla
        }

        public async Task<RoleClaimResponseDto> CreateRoleClaim(CreateRoleClaimRequestDto dto)
        {
            if (!_roleManager.SupportsRoleClaims) throw new Exception("Role claims not supported");

            var role = await _roleManager.FindByIdAsync(dto.RoleId).ConfigureAwait(false);
            if (role == null) throw new Exception("Role not Found");

            var claim = dto.MapToModel();
            var result = await _roleManager.AddClaimAsync(role, claim).ConfigureAwait(false);
            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
            return claim.MapToDto(role);
        }
        public async Task RemoveRoleClaim(RemoveRoleClaimRequestDto dto, CancellationToken cancel)
        {
            var role = await _roleManager.FindByIdAsync(dto.RoleId).ConfigureAwait(false);
            if (role == null) throw new Exception("Role not found");

            //var roleClaims = await _roleClaimStore.GetClaimsAsync(role, cancel).ConfigureAwait(false);
            var roleClaims = await _roleManager.GetClaimsAsync(role).ConfigureAwait(false);
            var claim = roleClaims.FirstOrDefault(cancel => cancel.Type == dto.Type);
            if (claim == null) throw new Exception("Claim not found");

            var result = await _roleManager.RemoveClaimAsync(role, claim).ConfigureAwait(false);
            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
        }
        public async Task<ListDto<RoleClaimResponseDto>> GetRoleClaims(string roleId, int page, int pageSize)
        {
            var role = await _roleManager.FindByIdAsync(roleId).ConfigureAwait(false);
            if (role == null) throw new Exception("Role not found");

            var roleClaims = await _roleManager.GetClaimsAsync(role).ConfigureAwait(false);
            return new ListDto<RoleClaimResponseDto>
            {
                Items = roleClaims.Select(x => x.MapToDto(role)).ToList(),
                Page = page,
                PageSize = pageSize,
                Total = roleClaims.Count
            };
        }
    }
}
