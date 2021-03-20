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

            //validators
            sc.AddTransient<IValidator<ApiResourceContract>, ApiResourceValidator>();
            sc.AddTransient<IValidator<ApiResourceClaimsContract>, ApiResourceClaimsValidator>();
            sc.AddTransient<IValidator<ApiResourceScopesContract>, ApiResourceScopesValidator>();
            sc.AddTransient<IValidator<ApiResourceSecretsContract>, ApiResourceSecretsValidator>();
            sc.AddTransient<IValidator<ApiResourcePropertiesContract>, ApiResourcePropertiesValidator>();
            sc.AddTransient<IValidator<ApiScopeContract>, ApiScopeValidator>();
            sc.AddTransient<IValidator<ApiScopeClaimsContract>, ApiScopeClaimsValidator>();
            sc.AddTransient<IValidator<ApiScopePropertiesContract>, ApiScopePropertiesValidator>();
            sc.AddTransient<ValidationFactory>();
        }
    }
}
