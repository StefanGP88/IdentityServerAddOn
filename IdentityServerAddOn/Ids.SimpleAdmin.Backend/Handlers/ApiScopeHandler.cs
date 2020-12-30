using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Backend.Handlers
{
    public class ApiScopeHandler : IApiScopeHandler
    {
        private readonly ConfigurationDbContext _confContext;
        public ApiScopeHandler(ConfigurationDbContext configurationDbContext)
        {
            _confContext = configurationDbContext;
        }

        public async Task<ApiScopeResponseDto> CreateApiScope(CreateApiScopeRequestDto dto, CancellationToken cancel)
        {
            var newScope = new ApiScope()
            {
                DisplayName = dto.DisplayName,
                Description = dto.Description,
                Emphasize = dto.Emphasize,
                Enabled = dto.Enabled,
                Name = dto.Name,
                Required = dto.Required,
                ShowInDiscoveryDocument = dto.ShowInDiscoveryDocument,
                Properties = dto.Properties.Select(x => new ApiScopeProperty
                {
                    Key = x.Key,
                    Value = x.Value
                }).ToList(),
                UserClaims = dto.Claims.Select(x => new ApiScopeClaim
                {
                    Type = x.Value
                }).ToList()
            };

            _ = await _confContext.ApiScopes.AddAsync(newScope, cancel).ConfigureAwait(false);
            await _confContext.SaveChangesAsync(false).ConfigureAwait(false);
            return new ApiScopeResponseDto();
        }

        public async Task<ListDto<ApiScopeResponseDto>> ReadAllApiScopes(int page, int pageSize, CancellationToken cancel)
        {
            var scopes = await _confContext.ApiScopes
                            .OrderBy(x => x.Name)
                            .Skip(page * pageSize)
                            .Take(pageSize)
                            .ToListAsync(cancel)
                            .ConfigureAwait(false);
            return new ListDto<ApiScopeResponseDto>
            {
                Page = page,
                PageSize = pageSize,
                Items = scopes.ConvertAll(x => MapToDto(x)),
                TotalItems = scopes.Count
            };
        }

        private ApiScopeResponseDto MapToDto(ApiScope apiScope)
        {
            if (apiScope == null) return null;
            return new ApiScopeResponseDto
            {
                DisplayName = apiScope.DisplayName,
                Emphasize = apiScope.Emphasize,
                Enabled = apiScope.Enabled,
                Id = apiScope.Id,
                Name = apiScope.Name,
                Required = apiScope.Required,
                ShowInDiscoveryDocument = apiScope.ShowInDiscoveryDocument,
                Description = apiScope.Description
            };
        }
    }
}
