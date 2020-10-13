using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend
{
    public class RoleHandler : IRoleHandler
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<RoleResponseDto> CreateRole(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName).ConfigureAwait(false))
                throw new Exception("Role Exists");

            var role = ConstructIdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role).ConfigureAwait(false);

            CheckResult(result);

            return role.MapToDto();
        }

        public async Task<ICollection<RoleResponseDto>> ReadAllRoles(CancellationToken cancel)
        {
            return await _roleManager.Roles
                .Select(x => x.MapToDto())
                .ToListAsync(cancel)
                .ConfigureAwait(false);
        }

        public async Task<RoleResponseDto> ReadRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id).ConfigureAwait(false);
            return role.MapToDto();
        }

        public async Task<RoleResponseDto> UpdateRole(UpdateRoleRequestDto dto)
        {
            var oldRole = await _roleManager.FindByIdAsync(dto.Id).ConfigureAwait(false);

            if (oldRole == null)
                throw new Exception("Role Does Not Exists");
            if (oldRole.ConcurrencyStamp != dto.ConcurrencyStamp)
                throw new Exception("ConcurrencyStamp has been changed");

            var roleUpdate = ConstructIdentityRole(dto.RoleName, dto.Id);
            var result = await _roleManager.UpdateAsync(roleUpdate).ConfigureAwait(false);

            CheckResult(result);

            return roleUpdate.MapToDto();
        }

        public async Task DeleteRole(DeleteRoleRequestDto dto)
        {
            var oldRole = await _roleManager.FindByIdAsync(dto.Id).ConfigureAwait(false);

            if (oldRole == null)
                throw new Exception("Role Does Not Exists");
            if (oldRole.ConcurrencyStamp != dto.ConcurrencyStamp)
                throw new Exception("ConcurrencyStamp has been changed");

            await _roleManager.DeleteAsync(oldRole).ConfigureAwait(false);
        }

        //add to a mapper instead
        private IdentityRole ConstructIdentityRole(string roleName)
        {
            return ConstructIdentityRole(roleName, Guid.NewGuid().ToString());
        }
        private IdentityRole ConstructIdentityRole(string roleName, string id)
        {
            return new IdentityRole
            {
                NormalizedName = _roleManager.KeyNormalizer.NormalizeName(roleName),
                Name = roleName,
                Id = id,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
        }

        private void CheckResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var Exceptions = result.Errors.Select(x => new Exception(x.Description));
                var aggregate = new AggregateException("Role creation failed", Exceptions);
                throw aggregate;
            }
        }
    }
}
