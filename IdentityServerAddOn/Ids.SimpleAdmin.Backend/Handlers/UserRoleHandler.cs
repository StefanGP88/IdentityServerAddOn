using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class UserRoleHandler : IUserRoleHandler
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleHandler(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task AddRoleToUser(CreateUserRoleRequestDto dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.RoleId).ConfigureAwait(false);
            if (role == null) throw new Exception("Role not found");

            var user = await _userManager.FindByIdAsync(dto.UserId).ConfigureAwait(false);
            if (user == null) throw new Exception("User not found");

            var result = await _userManager.AddToRoleAsync(user, role.Name).ConfigureAwait(false);
            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
        }

        public async Task RemoveRoleFromUser(RemoveUserRoleRequestDto dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.RoleId).ConfigureAwait(false);
            if (role == null) throw new Exception("Role not found");

            var user = await _userManager.FindByIdAsync(dto.UserId).ConfigureAwait(false);
            if (user == null) throw new Exception("User not found");

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name).ConfigureAwait(false);
            if (!result.Succeeded) throw new Exception(result.Errors.First().Description);
        }

        public async Task<ListDto<RoleResponseDto>> GetUserRoles(string userId, int page, int pageSize, CancellationToken cancel)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null) throw new Exception("User not found");

            var roleNames = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            return new ListDto<RoleResponseDto>
            {
                Items = await _roleManager.Roles
                    .Where(x => roleNames.Contains(x.Name))
                    .Select(x => x.MapToDto())
                    .ToListAsync(cancel)
                    .ConfigureAwait(false),
                Page = page,
                PageSize = pageSize,
                TotalItems = await _roleManager.Roles
                    .Where(x => roleNames.Contains(x.Name))
                    .CountAsync(cancel)
                    .ConfigureAwait(false)
            };
        }
    }
}
