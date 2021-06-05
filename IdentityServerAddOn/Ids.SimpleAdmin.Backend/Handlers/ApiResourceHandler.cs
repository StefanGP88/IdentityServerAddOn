using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class ApiResourceHandler : IHandler<ApiResourceContract, int?>
    {
        private readonly ConfigurationDbContext _confContext;
        private readonly IMapper<ApiResourceContract, ApiResource> _mapper;

        public ApiResourceHandler(ConfigurationDbContext configurationDbContext,
            IMapper<ApiResourceContract, ApiResource> mapper)
        {
            _confContext = configurationDbContext;
            _mapper = mapper;
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
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            return new ListDto<ApiResourceContract>()
            {
                Items = list.ConvertAll(_mapper.ToContract),
                Page = page,
                PageSize = pageSize,
                TotalItems = list.Count
            };
        }

        public async Task<ApiResourceContract> Get(int? id, CancellationToken cancel)
        {
            var model = await _confContext.ApiResources
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .Include(x => x.Scopes)
                .Include(x => x.Secrets)
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);
            return _mapper.ToContract(model);
        }

        public async Task<ListDto<ApiResourceContract>> Delete(int? id, int page, int pageSize, CancellationToken cancel)
        {
            var model = await _confContext.ApiResources
                .Where(x => x.Id == id)
                .FirstAsync(cancel)
                .ConfigureAwait(false);

            _confContext.ApiResources
                .Remove(model);

            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return await GetAll(page, pageSize, cancel).ConfigureAwait(false);
        }

        public async Task<ApiResourceContract> Create(ApiResourceContract dto, CancellationToken cancel)
        {
            var model = _mapper.ToModel(dto);
            model.Created = DateTime.UtcNow;

            await _confContext.ApiResources.AddAsync(model, cancel).ConfigureAwait(false);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return _mapper.ToContract(model);
        }

        public async Task<ApiResourceContract> Update(ApiResourceContract dto, CancellationToken cancel)
        {
            var model = await _confContext.ApiResources
                .Where(x => x.Id == dto.Id)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .Include(x => x.Scopes)
                .Include(x => x.Secrets)
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);

            model = _mapper.UpdateModel(model, dto);
            _confContext.ApiResources.Update(model);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);

            return _mapper.ToContract(model);
        }
    }
}
