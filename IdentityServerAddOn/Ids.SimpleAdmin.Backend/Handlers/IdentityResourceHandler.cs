﻿using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class IdentityResourceHandler : IHandler<IdentityResourceContract, int?>
    {
        private readonly ConfigurationDbContext _confContext;

        public IdentityResourceHandler(ConfigurationDbContext configurationDbContext)
        {
            _confContext = configurationDbContext;
        }

        public async Task<IdentityResourceContract> Create(IdentityResourceContract dto, CancellationToken cancel)
        {
            var model = dto.Adapt<IdentityResource>();
            model.Created = DateTime.UtcNow;

            await _confContext.IdentityResources.AddAsync(model, cancel).ConfigureAwait(false);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return model.Adapt<IdentityResourceContract>();
        }

        public async Task<ListDto<IdentityResourceContract>> Delete(int? id, int page, int pageSize, CancellationToken cancel)
        {
            var model = await _confContext.IdentityResources
                .Where(x => x.Id == id)
                .FirstAsync(cancel)
                .ConfigureAwait(false);

            _confContext.IdentityResources
                .Remove(model);

            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return await GetAll(page, pageSize, cancel).ConfigureAwait(false);
        }

        public async Task<IdentityResourceContract> Get(int? id, CancellationToken cancel)
        {
            return await _confContext.IdentityResources
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .ProjectToType<IdentityResourceContract>()
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);
        }

        public async Task<ListDto<IdentityResourceContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            var list = await _confContext.IdentityResources
                .AsNoTracking()
                .Skip(page * pageSize)
                .Take(pageSize)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .ProjectToType<IdentityResourceContract>()
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            return new ListDto<IdentityResourceContract>()
            {
                Items = list,
                Page = page,
                PageSize = pageSize,
                TotalItems = list.Count
            };
        }

        public async Task<IdentityResourceContract> Update(IdentityResourceContract dto, CancellationToken cancel)
        {
            var model = await _confContext.ApiResources
                .Where(x => x.Id == dto.Id)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .Include(x => x.Scopes)
                .Include(x => x.Secrets)
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);

            dto.Adapt(model);
            model.Updated = DateTime.UtcNow;

            _confContext.ApiResources.Update(model);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return model.Adapt<IdentityResourceContract>();
        }
    }
}