using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;
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
        private readonly IMapper<ValueClaimsContract, IdentityRoleClaim<string>> _claimsMapper;
        private readonly IMapper<RolesContract, IdentityRole> _rolesMapper;
        private readonly IdentityDbContext _dbContext;
        public RolesHandler(IdentityDbContext identityDbContext,
            IMapper<RolesContract, IdentityRole> rolesMapper,
            IMapper<ValueClaimsContract, IdentityRoleClaim<string>> claimsMapper)
        {
            _dbContext = identityDbContext;
            _rolesMapper = rolesMapper;
            _claimsMapper = claimsMapper;
        }
        public async Task<RolesContract> Create(RolesContract dto, CancellationToken cancel)
        {

            var model = _rolesMapper.ToModel(dto);
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
                .FirstOrDefaultAsync(x => x.Id == id, cancel)
                .ConfigureAwait(false);
            var contract = _rolesMapper.ToContract(model);

            if (!string.IsNullOrWhiteSpace(id))
            {
                var claimModels = await _dbContext.RoleClaims
                    .AsNoTracking()
                    .Where(x => x.RoleId == id)
                    .ToListAsync(cancel)
                    .ConfigureAwait(false);
                contract.Claims = claimModels?.ConvertAll(_claimsMapper.ToContract);
            }

            return contract;
        }

        public async Task<ListDto<RolesContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            var roleModel = await _dbContext.Roles
                .AsNoTracking()
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            var roleIds = roleModel.Select(x => x.Id);

            var roleClaimsModel = await _dbContext.RoleClaims
                .AsNoTracking()
                .Where(x => roleIds.Contains(x.RoleId))
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            return new ListDto<RolesContract>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = roleModel.Count,
                Items = roleModel.ConvertAll(x =>
                {
                    var contract = _rolesMapper.ToContract(x);
                    contract.Claims = roleClaimsModel
                        .Where(y => y.RoleId == x.Id)//DODO: why did I include this where ? is it still needed?
                        .Select(_claimsMapper.ToContract)
                        .ToList();
                    return contract;
                })
            };
        }

        public async Task<RolesContract> Update(RolesContract dto, CancellationToken cancel)
        {
            var model = await _dbContext.Roles
                .FirstOrDefaultAsync(x => x.Id == dto.Id, cancel)
                .ConfigureAwait(false);
            model = _rolesMapper.UpdateModel(model, dto);
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
            if (dto.Claims is null) return;

            var toAddRoleClaims = dto.Claims?
                .Where(x => x.Id == null)
                .ToList();

            var toAddRoleClaimsConverted = toAddRoleClaims?.ConvertAll(x =>
            {
                var converted = _claimsMapper.ToModel(x);
                converted.RoleId = roleId;
                return converted;
            });

            await _dbContext.RoleClaims
                .AddRangeAsync(toAddRoleClaimsConverted, cancel)
                .ConfigureAwait(false);
        }

        private void RemoveClaims(RolesContract dto, List<IdentityRoleClaim<string>> roleClaims)
        {
            var toRemoveRoleClaims = FindClaimsToRemove(roleClaims, dto.Claims);
            _dbContext.RoleClaims.RemoveRange(toRemoveRoleClaims);
        }

        private void UpdateClaims(RolesContract dto, List<IdentityRoleClaim<string>> roleClaims)
        {
            var toUpdateRoleClaims = FindClaimsToUpdate(roleClaims, dto.Claims);
            toUpdateRoleClaims = toUpdateRoleClaims.ConvertAll(x =>
            {
                var contract = dto.Claims?.Find(y => y.Id == x.Id);
                if (contract is null) return null;
                return _claimsMapper.ToModel(contract);
            });
            _dbContext.RoleClaims.UpdateRange(toUpdateRoleClaims.Where(x=> x is not null));
        }

        private static List<IdentityRoleClaim<string>> FindClaimsToRemove(
            List<IdentityRoleClaim<string>> dbList,
            List<ValueClaimsContract> dtoList)
        {
            var dtoIds = dtoList?.Select(x => x.Id);
            if (dtoIds is null) return dbList;

            return dbList
                .Where(x => !dtoIds.Contains(x.Id))
                .ToList();
        }

        private static List<IdentityRoleClaim<string>> FindClaimsToUpdate(
            List<IdentityRoleClaim<string>> dbList,
            List<ValueClaimsContract> dtoList)
        {
            var dtoIds = dtoList?.Select(x => x.Id);
            if (dtoIds is null) return dbList;
            return dbList
                .Where(x => dtoIds.Contains(x.Id))
                .ToList();
        }
    }
}
