using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NormalLibrary.Dtos;
using NormalLibrary.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NormalLibrary
{
    public class RoleHandler : IRoleHandler
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleHandler(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateRole(string roleName)
        {
            var role = ConstructIdentityRole(roleName);
            await CreateRole(role).ConfigureAwait(false);
        }

        public async Task CreateRole(IdentityRole role)
        {
            if (await _roleManager.RoleExistsAsync(role.Name).ConfigureAwait(false))
                throw new Exception("Role Exists");

            await _roleManager.CreateAsync(role).ConfigureAwait(false);
        }

        public async Task<ICollection<RoleResponseDto>> ReadAllRoles(CancellationToken cancel)
        {
            return await _roleManager.Roles
                .Select(x => (RoleResponseDto)x)
                .ToListAsync(cancel)
                .ConfigureAwait(false);
        }

        public async Task<RoleResponseDto> ReadRole(string id)
        {
            return (RoleResponseDto)await _roleManager.FindByIdAsync(id).ConfigureAwait(false);
        }

        public async Task UpdateRole()

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
    }
}
