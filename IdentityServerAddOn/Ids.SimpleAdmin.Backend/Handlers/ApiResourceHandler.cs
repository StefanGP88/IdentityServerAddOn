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
    public class ApiResourceHandler: IApiResourceHandler
    {
        private readonly ConfigurationDbContext _confContext;
        public ApiResourceHandler(ConfigurationDbContext configurationDbContext)
        {
            _confContext = configurationDbContext;
        }

        public async Task CreateApiResource(CreateApiResourceRequestDto dto, CancellationToken cancellation)
        {
            var resource = new ApiResource
            {
                AllowedAccessTokenSigningAlgorithms = dto.AllowedAccessTokenSigningAlgorithmes,
                Created = DateTime.UtcNow,
                Description = dto.Description,
                DisplayName = dto.DisplayName,
                Enabled = dto.Enabled,
                LastAccessed = DateTime.UtcNow,
                Name = dto.Name,
                NonEditable = dto.NonEditable,
                ShowInDiscoveryDocument = dto.ShowInDiscoveryDocument,
                Updated = null,
                Properties = dto.Properties.Select(x => new ApiResourceProperty
                {
                    Key = x.Value.Key,
                    Value = x.Value.PropertyValue
                }).ToList(),
                Scopes = dto.Scopes.Select(x => new ApiResourceScope
                {
                    Scope = x.Value
                }).ToList(),
                Secrets = dto.Secrets.Select(x => new ApiResourceSecret
                {
                    Created = DateTime.UtcNow,
                    Value = x.Value.Value,
                    Description = x.Value.Description,
                    Expiration = x.Value.Expiration,
                    Type = x.Value.Type
                }).ToList(),
                UserClaims = dto.Claims.Select(x => new ApiResourceClaim
                {
                    Type = x.Value
                }).ToList()
            };
            _ = await _confContext.ApiResources.AddAsync(resource, cancellation).ConfigureAwait(false);
            _ = await _confContext.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public async Task<ListDto<ApiResourceResponseDto>> ReadAll(int page, int pageSize, CancellationToken cancel)
        {
            var resources = await _confContext.ApiResources
                .OrderBy(x => x.Name)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Include(x=>x.UserClaims)
                .Include(x=>x.Secrets)
                .Include(x=>x.Scopes)
                .Include(x=>x.Properties)
                .ToListAsync(cancel)
                .ConfigureAwait(false);

            return new ListDto<ApiResourceResponseDto>
            {
                Items = resources.ConvertAll(x => new ApiResourceResponseDto
                {
                    AllowedAccessTokenSigningAlgorithmes = x.AllowedAccessTokenSigningAlgorithms,
                    Claims = x.UserClaims.ToDictionary(y => y.Id.ToString(), y => y.Type),
                    Created = x.Created,
                    Id = x.Id.ToString(),
                    Description = x.Description,
                    Properties = x.Properties.ToDictionary(y => y.Id.ToString(), y => new ApiResourcePropertyResponseDto
                    {
                        Key = y.Key,
                        PropertyValue = y.Value
                    }),
                    DisplayName = x.DisplayName,
                    Enabled = x.Enabled,
                    Name = x.Name,
                    NonEditable = x.NonEditable,
                    Scopes = x.Scopes.ToDictionary(y => y.Id.ToString(), y => y.Scope),
                    Secrets = x.Secrets.ToDictionary(y => y.Id.ToString(), y => new ApiResourceSecretsResponseDto
                    {
                        Created = y.Created,
                        Description = y.Description,
                        Expiration = y.Expiration ?? DateTime.MaxValue,
                        Value = y.Value,
                        Type = y.Type
                    }),
                    ShowInDiscoveryDocument = x.ShowInDiscoveryDocument
                }),
                Page = page,
                PageSize = pageSize,
                TotalItems = resources.Count
            };
        }
    }
}
