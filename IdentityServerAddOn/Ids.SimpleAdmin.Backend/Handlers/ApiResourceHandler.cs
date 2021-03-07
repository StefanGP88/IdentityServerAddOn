using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class ApiResourceHandler : IHandler<ApiResourceContract, int?>
    {
        private readonly ConfigurationDbContext _confContext;

        public ApiResourceHandler(ConfigurationDbContext configurationDbContext)
        {
            _confContext = configurationDbContext;
        }

        public async Task<ListDto<ApiResourceContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            var list = await _confContext.ApiResources
                .AsNoTracking()
                .Skip(page * pageSize)
                .Take(pageSize)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .Include(x => x.Scopes)
                .Include(x => x.Secrets)
                .ProjectToType<ApiResourceContract>()
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            return new ListDto<ApiResourceContract>()
            {
                Items = list,
                Page = page,
                PageSize = pageSize,
                TotalItems = list.Count
            };
        }

        public async Task<ApiResourceContract> Get(int? id, CancellationToken cancel)
        {
            return await _confContext.ApiResources
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .Include(x => x.Scopes)
                .Include(x => x.Secrets)
                .ProjectToType<ApiResourceContract>()
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);
        }

        public async Task<ListDto<ApiResourceContract>> Delete(int? id, int page, int pageSize, CancellationToken cancel)
        {
            var resource = await _confContext.ApiResources
                .Where(x => x.Id == id)
                .FirstAsync(cancel)
                .ConfigureAwait(false);

            _confContext.ApiResources
                .Remove(resource);

            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return await GetAll(page, pageSize, cancel).ConfigureAwait(false);
        }

        public async Task<ApiResourceContract> Create(ApiResourceContract dto, CancellationToken cancel)
        {
            var model = dto.Adapt<ApiResource>();
            await _confContext.ApiResources.AddAsync(model, cancel).ConfigureAwait(false);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return model.Adapt<ApiResourceContract>();
        }

        public async Task<ApiResourceContract> Update(ApiResourceContract dto, CancellationToken cancel)
        {
            var model = await _confContext.ApiResources
                .AsNoTracking()
                .Where(x => x.Id == dto.Id)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .Include(x => x.Scopes)
                .Include(x => x.Secrets)
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);

            dto.Adapt(model);
            _confContext.ApiResources.Update(model);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return model.Adapt<ApiResourceContract>();
        }
    }
}
