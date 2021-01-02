using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Dtos;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task DeleteApiScope(string id, CancellationToken token)
        {
            var scope = await _confContext.ApiScopes.FirstOrDefaultAsync(x => x.Id.ToString() == id, token).ConfigureAwait(false);
            if (scope == null) return;
            _confContext.ApiScopes.Remove(scope);
            await _confContext.SaveChangesAsync(token).ConfigureAwait(false);
        }

        public async Task<ListDto<ApiScopeResponseDto>> ReadAllApiScopes(int page, int pageSize, CancellationToken cancel)
        {
            var scopes = await _confContext.ApiScopes
                            .OrderBy(x => x.Name)
                            .Skip(page * pageSize)
                            .Include(x => x.UserClaims)
                            .Include(x => x.Properties)
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

        public async Task<ApiScopeResponseDto> UpdateApiScope(UpdateApiScopeRequestDto dto, CancellationToken cancellation)
        {
            var scope = await _confContext.ApiScopes
                .Include(x => x.UserClaims)
                .Include(x => x.Properties)
                .FirstOrDefaultAsync(x => x.Id == dto.Id)
                .ConfigureAwait(false);
            if (scope == null) throw new Exception("scope not found");

            scope.Description = dto.Description;
            scope.DisplayName = dto.DisplayName;
            scope.Emphasize = dto.Emphasize;
            scope.Enabled = dto.Enabled;
            scope.Name = dto.Name;
            scope.Required = dto.Required;
            scope.ShowInDiscoveryDocument = dto.ShowInDiscoveryDocument;

            scope.UserClaims.RemoveAll(x => !dto.Claims.ContainsKey(x.Id.ToString()));
            var toUpdateScope = scope.UserClaims.Where(x => dto.Claims.ContainsKey(x.Id.ToString())).ToList();
            var toAddScope = dto.Claims.Where(x => !toUpdateScope.Any(y => y.Id.ToString() == x.Key)).ToList();

            foreach (var item in toUpdateScope)
            {
                item.Type = dto.Claims[item.Id.ToString()];
            }

            foreach (var item in toAddScope)
            {
                scope.UserClaims.Add(new ApiScopeClaim
                {
                    ScopeId = dto.Id,
                    Type = item.Value
                });
            }

            scope.Properties.RemoveAll(x=> !dto.Properties.ContainsKey(x.Id.ToString()));
            var toUpdateProperty = scope.Properties.Where(x => dto.Properties.ContainsKey(x.Id.ToString())).ToList();
            var toAddProperty = dto.Properties.Where(x => !toUpdateProperty.Any(y => y.Id.ToString() == x.Key)).ToList();

            foreach(var item in toUpdateProperty)
            {
                item.Key = dto.Properties[item.Id.ToString()].Key;
                item.Value = dto.Properties[item.Id.ToString()].PropertyValue;
            }

            foreach(var item in toAddProperty)
            {
                scope.Properties.Add(new ApiScopeProperty
                {
                    Key = item.Value.Key,
                    Value = item.Value.PropertyValue
                });
            }

            _confContext.ApiScopes.Update(scope);

            await _confContext.SaveChangesAsync(cancellation).ConfigureAwait(false);

            return new ApiScopeResponseDto();
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
                Description = apiScope.Description,
                Claims = apiScope.UserClaims.ToDictionary(x => x.Id, x => x.Type),
                Properties = apiScope.Properties.ToDictionary(x => x.Id.ToString(), x => new ApiScopePropertyResponseDto { Key = x.Key, PropertyValue = x.Value })
            };
        }
    }
}
