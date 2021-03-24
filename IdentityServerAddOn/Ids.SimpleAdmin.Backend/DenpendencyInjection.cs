using Microsoft.Extensions.DependencyInjection;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Handlers;
using Ids.SimpleAdmin.Contracts;
using FluentValidation;
using Ids.SimpleAdmin.Backend.Validators;

namespace Ids.SimpleAdmin.Backend
{
    public static class DenpendencyInjection
    {
        public static void AddIdentityServerAddOn(this IServiceCollection sc)
        {
            //handlers
            sc.AddScoped<IHandler<ApiResourceContract, int?>, ApiResourceHandler>();
            sc.AddScoped<IHandler<ApiScopeContract, int?>, ApiScopeHandler>();
            sc.AddScoped<IHandler<IdentityResourceContract, int?>, IdentityResourceHandler>();
            sc.AddScoped<IHandler<ClientsContract, int?>, ClientHandler>();

            //validators
            sc.AddTransient<IValidator<ApiResourceContract>, ApiResourceValidator>();
            sc.AddTransient<IValidator<ApiResourceClaimsContract>, ApiResourceClaimsValidator>();
            sc.AddTransient<IValidator<ApiResourceScopesContract>, ApiResourceScopesValidator>();
            sc.AddTransient<IValidator<ApiResourceSecretsContract>, ApiResourceSecretsValidator>();
            sc.AddTransient<IValidator<ApiResourcePropertiesContract>, ApiResourcePropertiesValidator>();
            sc.AddTransient<IValidator<ApiScopeContract>, ApiScopeValidator>();
            sc.AddTransient<IValidator<ApiScopeClaimsContract>, ApiScopeClaimsValidator>();
            sc.AddTransient<IValidator<ApiScopePropertiesContract>, ApiScopePropertiesValidator>();
            sc.AddTransient<IValidator<IdentityResourceContract>, IdentityResourceValidator>();
            sc.AddTransient<IValidator<IdentityResourceClaimsContract>, IdentityResourceClaimsValidator>();
            sc.AddTransient<IValidator<IdentityResourcePropertiesContract>, IdentityResourcePropertiesValidator>();
            sc.AddTransient<IValidator<ClientsContract>, ClientValidator>();
            sc.AddTransient<IValidator<ClientSecretsContract>, ClientSecretsValidator>();
            sc.AddTransient<IValidator<ClientScopeContract>, ClientScopesValidator>();
            sc.AddTransient<IValidator<ClientRedirectUriContract>, ClientRedirectUrisValidator>();
            sc.AddTransient<IValidator<ClientPropertiesContract>, ClientPropertiesValidator>();
            sc.AddTransient<IValidator<ClientPostLogoutRedirectUrisContract>, ClientPostLogoutRedirectUrisValidator>();
            sc.AddTransient<IValidator<ClientIdPRestrictionsContract>, ClientIdPRestrictionsValidator>();
            sc.AddTransient<IValidator<ClientGrantTypesContract>, ClientGrantTypeValidator>();
            sc.AddTransient<IValidator<ClientCordOriginsContract>, ClientCorsOriginsValidator>();
            sc.AddTransient<IValidator<ClientClaimsContract>, ClientClaimsValidator>();
            sc.AddTransient<ValidationFactory>();
        }
    }
}
