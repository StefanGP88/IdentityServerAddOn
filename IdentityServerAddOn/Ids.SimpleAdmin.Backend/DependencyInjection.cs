using FluentValidation;
using IdentityServer4.EntityFramework.Entities;
using Ids.SimpleAdmin.Backend.Handlers;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Mappers;
using Ids.SimpleAdmin.Backend.Mappers.Interfaces;
using Ids.SimpleAdmin.Backend.Validators;
using Ids.SimpleAdmin.Contracts;
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
            services.TryAddTransient<IValidator<ApiResourceClaimsContract>, ApiResourceClaimsValidator>();
            services.TryAddTransient<IValidator<ApiResourceScopesContract>, ApiResourceScopesValidator>();
            services.TryAddTransient<IValidator<ApiResourceSecretsContract>, ApiResourceSecretsValidator>();
            services.TryAddTransient<IValidator<ApiResourcePropertiesContract>, ApiResourcePropertiesValidator>();
            services.TryAddTransient<IValidator<ApiScopeContract>, ApiScopeValidator>();
            services.TryAddTransient<IValidator<ApiScopeClaimsContract>, ApiScopeClaimsValidator>();
            services.TryAddTransient<IValidator<ApiScopePropertiesContract>, ApiScopePropertiesValidator>();
            services.TryAddTransient<IValidator<IdentityResourceContract>, IdentityResourceValidator>();
            services.TryAddTransient<IValidator<IdentityResourceClaimsContract>, IdentityResourceClaimsValidator>();
            services.TryAddTransient<IValidator<IdentityResourcePropertiesContract>, IdentityResourcePropertiesValidator>();
            services.TryAddTransient<IValidator<ClientsContract>, ClientValidator>();
            services.TryAddTransient<IValidator<ClientSecretsContract>, ClientSecretsValidator>();
            services.TryAddTransient<IValidator<ClientScopeContract>, ClientScopesValidator>();
            services.TryAddTransient<IValidator<ClientRedirectUriContract>, ClientRedirectUrisValidator>();
            services.TryAddTransient<IValidator<ClientPropertiesContract>, ClientPropertiesValidator>();
            services.TryAddTransient<IValidator<ClientPostLogoutRedirectUrisContract>, ClientPostLogoutRedirectUrisValidator>();
            services.TryAddTransient<IValidator<ClientIdPRestrictionsContract>, ClientIdPRestrictionsValidator>();
            services.TryAddTransient<IValidator<ClientGrantTypesContract>, ClientGrantTypeValidator>();
            services.TryAddTransient<IValidator<ClientCorsOriginsContract>, ClientCorsOriginsValidator>();
            services.TryAddTransient<IValidator<ClientClaimsContract>, ClientClaimsValidator>();
            services.TryAddTransient<IValidator<RolesContract>, RolesValidator>();
            services.TryAddTransient<IValidator<RoleClaimsContract>, RoleClaimsValidator>();
            services.TryAddTransient<IValidator<UserContract>, UserValidator>();
            services.TryAddTransient<IValidator<UserClaimsContract>, UserClaimsValidator>();
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
            services.TryAddScoped<IMapper<ClientPropertiesContract, ClientProperty>, ClientPropertyMapper>();
            services.TryAddScoped<IMapper<ClientScopeContract, ClientScope>, ClientScopeMapper>();
            services.TryAddScoped<IMapper<ClientSecretsContract, ClientSecret>, ClientSecretsMapper>();
            services.TryAddScoped<IMapper<ClientGrantTypesContract, ClientGrantType>, GrantTypeMapper>();
            services.TryAddScoped<IMapper<ClientRedirectUriContract, ClientRedirectUri>, RedirectUriMapper>();
            services.TryAddScoped<IMapper<ClientPostLogoutRedirectUrisContract, ClientPostLogoutRedirectUri>, PostLogoutRedirectUri>();
            services.TryAddScoped<IMapper<ApiResourceContract, ApiResource>, ApiResourceMapper>();
            services.TryAddScoped<IMapper<ApiResourceClaim, ApiResourceClaimsContract>, ApiResourceClaimsMapper>();
            services.TryAddScoped<IMapper<ApiResourceProperty, ApiResourcePropertiesContract>, ApiResourcePropertiesMapper>();
            services.TryAddScoped<IMapper<ApiResourceScope, ApiResourceScopesContract>, ApiResourceScopesMapper>();
            services.TryAddScoped<IMapper<ApiResourceSecret, ApiResourceSecretsContract>, ApiResourceSecretsMapper>();
            //services.TryAddScoped<,>();
        }
    }
}
