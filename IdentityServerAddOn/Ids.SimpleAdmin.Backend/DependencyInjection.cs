using FluentValidation;
using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Handlers;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Backend.Validators;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ids.SimpleAdmin.Backend
{
    public static class DependencyInjection
    {
        public static void AddSimpleAdminBackend(this IServiceCollection services)
        {
            RegisterHandlers(services);
            RegisterValidators(services);
            RegisterMappers(services);
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            services.TryAddTransient<IValidator<ApiResourceContract>, ApiResourceValidator>();
            services.TryAddTransient<IValidator<ApiScopeContract>, ApiScopeValidator>();
            services.TryAddTransient<IValidator<IdentityResourceContract>, IdentityResourceValidator>();
            services.TryAddTransient<IValidator<ClientsContract>, ClientValidator>();
            services.TryAddTransient<IValidator<ClientSecretsContract>, ClientSecretsValidator>();
            services.TryAddTransient<IValidator<ClientRedirectUriContract>, ClientRedirectUrisValidator>();
            services.TryAddTransient<IValidator<ClientPostLogoutRedirectUrisContract>, ClientPostLogoutRedirectUrisValidator>();
            services.TryAddTransient<IValidator<ClientIdPRestrictionsContract>, ClientIdPRestrictionsValidator>();
            services.TryAddTransient<IValidator<ClientGrantTypesContract>, ClientGrantTypeValidator>();
            services.TryAddTransient<IValidator<ClientCorsOriginsContract>, ClientCorsOriginsValidator>();
            services.TryAddTransient<IValidator<RolesContract>, RolesValidator>();
            services.TryAddTransient<IValidator<UserContract>, UserValidator>();
            services.TryAddTransient<IValidator<SecretsContract>, SecretValidator>();
            services.TryAddTransient<IValidator<ClaimsContract>, ClaimValidator>();
            services.TryAddTransient<IValidator<ScopeContract>, ScopeValidator>();
            services.TryAddTransient<IValidator<PropertyContract>, PropertyValidator>();
            services.TryAddTransient<IValidator<ValueClaimsContract>, ValueClaimValidator>();
            services.TryAddTransient<IValidator<ClientClaimsContract>, ValueClaimValidator>();
            services.TryAddTransient<ValidationFactory>();
        }

        private static void RegisterHandlers(IServiceCollection services)
        {
            services.TryAddScoped<IHandler<ApiResourceContract, int?>, ApiResourceHandler>();
            services.TryAddScoped<IHandler<ApiScopeContract, int?>, ApiScopeHandler>();
            services.TryAddScoped<IHandler<IdentityResourceContract, int?>, IdentityResourceHandler>();
            services.TryAddScoped<IHandler<ClientsContract, int?>, ClientHandler>();
            services.TryAddScoped<IHandler<RolesContract, string>, RolesHandler>();
            services.TryAddScoped<IHandler<UserContract, string>, UserHandler>();
        }

        private static void RegisterMappers(IServiceCollection services)
        {
            services.TryAddScoped<IMapper<ClientsContract, Client>, ClientMapper>();
            services.TryAddScoped<IMapper<ClientIdPRestrictionsContract, ClientIdPRestriction>, IdPRestrictionMapper>();
            services.TryAddScoped<IMapper<ClientClaimsContract, ClientClaim>, ClientClaimMapper>();
            services.TryAddScoped<IMapper<ClientCorsOriginsContract, ClientCorsOrigin>, CorsOriginMapper>();
            services.TryAddScoped<IMapper<PropertyContract, ClientProperty>, ClientPropertyMapper>();
            services.TryAddScoped<IMapper<ScopeContract, ClientScope>, ClientScopeMapper>();
            services.TryAddScoped<IMapper<ClientSecretsContract, ClientSecret>, ClientSecretsMapper>();
            services.TryAddScoped<IMapper<ClientGrantTypesContract, ClientGrantType>, GrantTypeMapper>();
            services.TryAddScoped<IMapper<ClientRedirectUriContract, ClientRedirectUri>, RedirectUriMapper>();
            services.TryAddScoped<IMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri>, PostLogoutRedirectUri>();
            services.TryAddScoped<IMapper<ApiResourceContract, ApiResource>, ApiResourceMapper>();
            services.TryAddScoped<IMapper<ClaimsContract, ApiResourceClaim>, ApiResourceClaimsMapper>();
            services.TryAddScoped<IMapper<PropertyContract,ApiResourceProperty>, ApiResourcePropertiesMapper>();
            services.TryAddScoped<IMapper<ScopeContract, ApiResourceScope>, ApiResourceScopesMapper>();
            services.TryAddScoped<IMapper<ApiResourceSecretsContract,ApiResourceSecret>, ApiResourceSecretsMapper>();
            services.TryAddScoped<IMapper<ApiScopeContract, ApiScope>, ApiScopeMapper>();
            services.TryAddScoped<IMapper<ClaimsContract, ApiScopeClaim>, ApiScopeClaimMapper>();
            services.TryAddScoped<IMapper<PropertyContract, ApiScopeProperty>, ApiScopePropertyMapper>();
            services.TryAddScoped<IMapper<IdentityResourceContract, IdentityResource>, IdentityResourceMapper>();
            services.TryAddScoped<IMapper<ClaimsContract, IdentityResourceClaim>, IdentityResourceClaimMapper>();
            services.TryAddScoped<IMapper<PropertyContract, IdentityResourceProperty>, IdentityResourcePropertyMapper>();
            services.TryAddScoped<IMapper<RolesContract, IdentityRole>, RolesMapper>();
            services.TryAddScoped<IMapper<ValueClaimsContract, IdentityRoleClaim<string>>, IdentityRoleClaimMapper>();
            services.TryAddScoped<IMapper<UserContract, IdentityUser>, UserMapper>();
            services.TryAddScoped<IMapper<ValueClaimsContract, IdentityUserClaim<string>>, UserClaimsmapper>();
        }
    }
}
