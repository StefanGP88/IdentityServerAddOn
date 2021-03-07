using IdentityServer4.EntityFramework.DbContexts;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<ListDto<ApiResourceContract>> Delete(int? id, int page, int pageSize, CancellationToken cancel)
        {
            return null;
        }

        public async Task<ApiResourceContract> Create(ApiResourceContract dto, int page, int pageSize, CancellationToken cancel)
        {
            return null;
        }

        public async Task<ApiResourceContract> Get(int? id, int page, int pageSize, CancellationToken cancel)
        {
            return null;
        }

        public async Task<ApiResourceContract> Update(ApiResourceContract dto, int page, int pageSize, CancellationToken cancel)
        {
            return null;
        }
    }
}
