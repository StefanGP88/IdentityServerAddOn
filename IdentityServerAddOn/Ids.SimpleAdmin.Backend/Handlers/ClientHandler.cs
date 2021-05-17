using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Contracts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class ClientHandler : IHandler<ClientsContract, int?>
    {
        private readonly ConfigurationDbContext _confContext;

        private readonly IMapper<ClientsContract, Client> _mapper;
        public ClientHandler(ConfigurationDbContext configurationDbContext,
            IMapper<ClientsContract, Client> mapper)
        {
            _confContext = configurationDbContext;
            _mapper = mapper;
        }
        public async Task<ClientsContract> Create(ClientsContract dto, CancellationToken cancel)
        {
            var model = dto.Adapt<Client>();
            await _confContext.Clients.AddAsync(model, cancel).ConfigureAwait(false);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return model.Adapt<ClientsContract>();
        }

        public async Task<ListDto<ClientsContract>> Delete(int? id, int page, int pageSize, CancellationToken cancel)
        {
            var model = await _confContext.Clients
                .Where(x => x.Id == id)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.Claims)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.Properties)
                .Include(x => x.AllowedScopes)
                .Include(x => x.ClientSecrets)
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .FirstAsync(cancel)
                .ConfigureAwait(false);

            _confContext.Clients
                .Remove(model);

            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return await GetAll(page, pageSize, cancel).ConfigureAwait(false);
        }

        public async Task<ClientsContract> Get(int? id, CancellationToken cancel)
        {
            return await _confContext.Clients
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.Claims)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.Properties)
                .Include(x => x.AllowedScopes)
                .Include(x => x.ClientSecrets)
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .ProjectToType<ClientsContract>()
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);
        }

        public async Task<ListDto<ClientsContract>> GetAll(int page, int pageSize, CancellationToken cancel)
        {
            var list = await _confContext.Clients
                .AsNoTracking()
                .Skip(page * pageSize)
                .Take(pageSize)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.Claims)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.Properties)
                .Include(x => x.AllowedScopes)
                .Include(x => x.ClientSecrets)
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .ProjectToType<ClientsContract>()
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            return new ListDto<ClientsContract>()
            {
                Items = list,
                Page = page,
                PageSize = pageSize,
                TotalItems = list.Count
            };
        }

        public async Task<ClientsContract> Update(ClientsContract dto, CancellationToken cancel)
        {
            var model = await _confContext.Clients
                .Where(x => x.Id == dto.Id)
                .Include(x => x.IdentityProviderRestrictions)
                .Include(x => x.Claims)
                .Include(x => x.AllowedCorsOrigins)
                .Include(x => x.Properties)
                .Include(x => x.AllowedScopes)
                .Include(x => x.ClientSecrets)
                .Include(x => x.AllowedGrantTypes)
                .Include(x => x.RedirectUris)
                .Include(x => x.PostLogoutRedirectUris)
                .FirstOrDefaultAsync(cancel)
                .ConfigureAwait(false);

            dto.Adapt(model);
            _confContext.Clients.Update(model);
            await _confContext.SaveChangesAsync(cancel).ConfigureAwait(false);
            return model.Adapt<ClientsContract>();
        }
    }


}
