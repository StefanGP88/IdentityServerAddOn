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
    public class IdentityResourceHandler : IHandler<IdentityResourceContract, int?>
    {
        private readonly ConfigurationDbContext _confContext;
        private readonly IMapper<IdentityResourceContract, IdentityResource> _mapper;

        public IdentityResourceHandler(ConfigurationDbContext configurationDbContext,
            IMapper<IdentityResourceContract, IdentityResource> mapper)
        {
            _confContext = configurationDbContext;
            _mapper = mapper;
        }

        public async Task<IdentityResourceContract> Create(IdentityResourceContract dto, CancellationToken cancel)
        {
            var model = _mapper.ToModel(dto);
            await _confContext.IdentityResources.AddAsync(model, cancel).ConfigureAwait(false);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return _mapper.ToContract(model);
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
            var model = await _confContext.IdentityResources
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);
            return _mapper.ToContract(model);
        }

        public async Task<ListDto<IdentityResourceContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            var list = await _confContext.IdentityResources
                .AsNoTracking()
                .Skip(page * pageSize)
                .Take(pageSize)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            return new ListDto<IdentityResourceContract>()
            {
                Items = list?.ConvertAll(_mapper.ToContract),
                Page = page,
                PageSize = pageSize,
                TotalItems = list.Count
            };
        }

        public async Task<IdentityResourceContract> Update(IdentityResourceContract dto, CancellationToken cancel)
        {
            var model = await _confContext.IdentityResources
                .Where(x => x.Id == dto.Id)
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);

            model = _mapper.UpdateModel(model, dto);
            _confContext.IdentityResources.Update(model);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return _mapper.ToContract(model);
        }
    }
}