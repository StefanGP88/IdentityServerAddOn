using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class ApiScopeHandler : IHandler<ApiScopeContract, int?>
    {
        private readonly ConfigurationDbContext _confContext;
        private readonly IMapper<ApiScopeContract, ApiScope> _mapper;

        public ApiScopeHandler(ConfigurationDbContext configurationDbContext,
            IMapper<ApiScopeContract, ApiScope> mapper)
        {
            _confContext = configurationDbContext;
            _mapper = mapper;
        }
        public async Task<ApiScopeContract> Create(ApiScopeContract dto, CancellationToken cancel)
        {
            var model = _mapper.ToModel(dto);
            await _confContext.ApiScopes.AddAsync(model, cancel).ConfigureAwait(false);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return _mapper.ToContract(model);
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
            var model = await _confContext.ApiScopes
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);
            return _mapper.ToContract(model);
        }

        public async Task<ListDto<ApiScopeContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            var list = await _confContext.ApiScopes
                .AsNoTracking()
                .Skip(page * pageSize)
                .Take(pageSize)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            return new ListDto<ApiScopeContract>()
            {
                Items = list?.ConvertAll(_mapper.ToContract),
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

            model = _mapper.UpdateModel(model, dto);
            _confContext.ApiScopes.Update(model);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return _mapper.ToContract(model);
        }
    }
}