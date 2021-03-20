using IdentityServer4.EntityFramework.DbContexts;
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
    public class ApiScopeHandler : IHandler<ApiScopeContract, int?>
    {
        private readonly ConfigurationDbContext _confContext;

        public ApiScopeHandler(ConfigurationDbContext configurationDbContext)
        {
            _confContext = configurationDbContext;
        }
        public async Task<ApiScopeContract> Create(ApiScopeContract dto, CancellationToken cancel)
        {
            var model = dto.Adapt<ApiScope>();
            await _confContext.ApiScopes.AddAsync(model, cancel).ConfigureAwait(false);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return model.Adapt<ApiScopeContract>();
        }

        public async Task<ListDto<ApiScopeContract>> Delete(int? id, int page, int pageSize, CancellationToken cancel)
        {
            var model = await _confContext.ApiScopes
                .Where(x => x.Id == id)
                .FirstAsync(cancel)
                .ConfigureAwait(false);

            _confContext.ApiScopes
                .Remove(model);

            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return await GetAll(page, pageSize, cancel).ConfigureAwait(false);
        }

        public async Task<ApiScopeContract> Get(int? id, CancellationToken cancel)
        {
            return await _confContext.ApiScopes
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .ProjectToType<ApiScopeContract>()
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);
        }

        public async Task<ListDto<ApiScopeContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            var list = await _confContext.ApiScopes
                .AsNoTracking()
                .Skip(page * pageSize)
                .Take(pageSize)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .ProjectToType<ApiScopeContract>()
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            return new ListDto<ApiScopeContract>()
            {
                Items = list,
                Page = page,
                PageSize = pageSize,
                TotalItems = list.Count
            };
        }

        public async Task<ApiScopeContract> Update(ApiScopeContract dto, CancellationToken cancel)
        {
            var model = await _confContext.ApiScopes
                .Where(x => x.Id == dto.Id)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);

            dto.Adapt(model);
            _confContext.ApiScopes.Update(model);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return model.Adapt<ApiScopeContract>();
        }
    }
}
