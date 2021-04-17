using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class RolesHandler : IHandler<RolesContract, string>
    {
        //TODO: replace with role manager
        private readonly IdentityDbContext _dbContext;
        public RolesHandler(IdentityDbContext identityDbContext, TestManager testManager)
        {
            _dbContext = identityDbContext;
        }
        public async Task<RolesContract> Create(RolesContract dto, CancellationToken cancel)
        {
            dto.NormalizedName = dto.Name.ToUpperInvariant();
            dto.ConcurrencyStamp = Guid.NewGuid().ToString();
            dto.Id = Guid.NewGuid().ToString();

            var model = dto.Adapt<IdentityRole>();
            _ = await _dbContext.Roles.AddAsync(model, cancel).ConfigureAwait(false);

            await AddClaims(dto, model.Id, cancel).ConfigureAwait(false);

            _ = await _dbContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return await Get(model.Id, cancel).ConfigureAwait(false);
        }

        public async Task<ListDto<RolesContract>> Delete(string id, int page, int pageSize, CancellationToken cancel)
        {
            var model = await _dbContext.Roles
                .FirstOrDefaultAsync(x => x.Id == id, cancel)
                .ConfigureAwait(false);
            var roleClaims = await _dbContext.RoleClaims
                .Where(x => x.RoleId == id)
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            _dbContext.Roles.Remove(model);
            _dbContext.RoleClaims.RemoveRange(roleClaims);

            await _dbContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return await GetAll(page, pageSize, cancel).ConfigureAwait(false);
        }

        public async Task<RolesContract> Get(string id, CancellationToken cancel)
        {
            var model = await _dbContext.Roles
                .AsNoTracking()
                .ProjectToType<RolesContract>()
                .FirstOrDefaultAsync(x => x.Id == id, cancel)
                .ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(id))
            {
                model.RoleClaims = await _dbContext.RoleClaims
                    .AsNoTracking()
                    .Where(x => x.RoleId == id)
                    .ProjectToType<RoleClaimsContract>()
                    .ToListAsync(cancel)
                    .ConfigureAwait(false);
            }

            return model;
        }

        public async Task<ListDto<RolesContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            var roles = await _dbContext.Roles
                .AsNoTracking()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ProjectToType<RolesContract>()
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            var roleIds = roles.Select(x => x.Id);

            var roleClaims = await _dbContext.RoleClaims
                .AsNoTracking()
                .Where(x => roleIds.Contains(x.RoleId))
                .ProjectToType<RoleClaimsContract>()
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            return new ListDto<RolesContract>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = roles.Count,
                Items = roles.ConvertAll(x =>
                {
                    x.RoleClaims = roleClaims
                        .Where(y => y.RoleId == x.Id)
                        .ToList();
                    return x;
                })
            };
        }

        public async Task<RolesContract> Update(RolesContract dto, CancellationToken cancel)
        {
            var model = await _dbContext.Roles
                .FirstOrDefaultAsync(x => x.Id == dto.Id, cancel)
                .ConfigureAwait(false);

            if (model.ConcurrencyStamp != dto.ConcurrencyStamp) throw new Exception("Concurrencystamp outdated"); //TODO: should propbly be in the validation class as a validation rule

            dto.NormalizedName = dto.Name.ToUpperInvariant();
            dto.ConcurrencyStamp = Guid.NewGuid().ToString();

            dto.Adapt(model);
            _dbContext.Roles.Update(model);

            var roleClaims = await _dbContext.RoleClaims
                 .Where(x => x.RoleId == dto.Id)
                 .ToListAsync(cancel)
                 .ConfigureAwait(false);

            RemoveClaims(dto, roleClaims);
            UpdateClaims(dto, roleClaims);
            await AddClaims(dto, model.Id, cancel).ConfigureAwait(false);

            await _dbContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return await Get(model.Id, cancel).ConfigureAwait(false);
        }

        private async Task AddClaims(RolesContract dto, string roleId, CancellationToken cancel)
        {
            var toAddRoleClaims = dto.RoleClaims
                .Where(x => x.Id == null)
                .ToList();

            var toAddRoleClaimsConverted = toAddRoleClaims.ConvertAll(x =>
            {
                var converted = x.Adapt<IdentityRoleClaim<string>>();
                converted.RoleId = roleId;
                return converted;
            });

            await _dbContext.RoleClaims
                .AddRangeAsync(toAddRoleClaimsConverted, cancel)
                .ConfigureAwait(false);
        }

        private void RemoveClaims(RolesContract dto, List<IdentityRoleClaim<string>> roleClaims)
        {
            var toRemoveRoleClaims = FindClaimsToRemove(roleClaims, dto.RoleClaims);
            _dbContext.RoleClaims.RemoveRange(toRemoveRoleClaims);
        }

        private void UpdateClaims(RolesContract dto, List<IdentityRoleClaim<string>> roleClaims)
        {
            var toUpdateRoleClaims = FindClaimsToUpdate(roleClaims, dto.RoleClaims);
            toUpdateRoleClaims = toUpdateRoleClaims.ConvertAll(x =>
            {
                var contract = dto.RoleClaims.Find(y => y.Id == x.Id);
                contract.Adapt(x);
                return x;
            });
            _dbContext.RoleClaims.UpdateRange(toUpdateRoleClaims);
        }

        private static List<IdentityRoleClaim<string>> FindClaimsToRemove(
            List<IdentityRoleClaim<string>> dbList,
            List<RoleClaimsContract> dtoList)
        {
            var dtoIds = dtoList.Select(x => x.Id);
            return dbList
                .Where(x => !dtoIds.Contains(x.Id))
                .ToList();
        }

        private static List<IdentityRoleClaim<string>> FindClaimsToUpdate(
            List<IdentityRoleClaim<string>> dbList,
            List<RoleClaimsContract> dtoList)
        {
            var dtoIds = dtoList.Select(x => x.Id);
            return dbList
                .Where(x => dtoIds.Contains(x.Id))
                .ToList();
        }
    }
}
