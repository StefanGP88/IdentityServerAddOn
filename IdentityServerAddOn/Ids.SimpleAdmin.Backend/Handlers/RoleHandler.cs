using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class RoleHandler : IRoleHandler
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<RoleResponseDto> CreateRole(CreateRoleRequestDto dto)
        {
            if (await _roleManager.RoleExistsAsync(dto.RoleName).ConfigureAwait(false))
                throw new Exception("Role Exists");

            var role = ConstructIdentityRole(dto.RoleName);
            var result = await _roleManager.CreateAsync(role).ConfigureAwait(false);

            CheckResult(result);
            await UpdateUserClaims(role, dto.Claims).ConfigureAwait(false);

            return role.MapToDto();
        }

        public async Task<ListDto<RoleResponseDto>> ReadRoles(int page, int pageSize, CancellationToken cancel)
        {
            var roles = await _roleManager.Roles
                        .OrderBy(x => x.Name)
                        .Skip(page * pageSize)
                        .Take(pageSize)
                        .ToListAsync(cancel)
                        .ConfigureAwait(false);

            var result = new ListDto<RoleResponseDto>
            {
                Items = new List<RoleResponseDto>(),
                Page = page,
                PageSize = pageSize,
                TotalItems = roles.Count
            };

            foreach (var item in roles)
            {
                var role = item.MapToDto();
                role.Claims = (List<Claim>)await _roleManager.GetClaimsAsync(item).ConfigureAwait(false);
                result.Items.Add(role);
            }
            return result;
        }

        public async Task<ListDto<RoleResponseDto>> ReadAllRoles(CancellationToken cancel)
        {
            var roleCount = await _roleManager.Roles
                        .CountAsync(cancel)
                        .ConfigureAwait(false);
            return new ListDto<RoleResponseDto>
            {
                Items = await _roleManager.Roles
                        .OrderBy(x => x.Name)
                        .Select(x => x.MapToDto())
                        .ToListAsync(cancel)
                        .ConfigureAwait(false),
                Page = 0,
                PageSize = roleCount,
                TotalItems = roleCount
            };
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

            oldRole = UpdateIdentityRole(oldRole, dto.RoleName);
            var result = await _roleManager.UpdateAsync(oldRole).ConfigureAwait(false);

            CheckResult(result);
            await UpdateUserClaims(oldRole, dto.Claims).ConfigureAwait(false);
            return oldRole.MapToDto();
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
        private IdentityRole UpdateIdentityRole(IdentityRole role, string update)
        {
            if (role == null) return null;
            role.Name = update;
            role.NormalizedName = _roleManager.KeyNormalizer.NormalizeName(update);
            role.ConcurrencyStamp = Guid.NewGuid().ToString();
            return role;
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

        private async Task UpdateUserClaims(IdentityRole role, Dictionary<string, string> claims)
        {
            var existingClaims = await _roleManager.GetClaimsAsync(role).ConfigureAwait(false);

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

            foreach (var item in toRemove)
            {
                var removeClaimResult = await _roleManager.RemoveClaimAsync(role, item).ConfigureAwait(false);
                if (!removeClaimResult.Succeeded)
                {
                    throw new Exception("Could not remove claims");
                }
            }
            foreach (var item in toAdd)
            {
                var addClaimResult = await _roleManager.AddClaimAsync(role, item).ConfigureAwait(false);
                if (!addClaimResult.Succeeded)
                {
                    throw new Exception("Could not add claims");
                }
            }
        }
    }
}
