using FluentValidation;
using Ids.SimpleAdmin.Backend.Handlers;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Validators;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ids.SimpleAdmin.Backend
{
    public static class DependencyInjection
    {
        public static void AddSimpleAdminBackend(this IServiceCollection services)
        {
            //handlers
            services.TryAddScoped<IHandler<ApiResourceContract, int?>, ApiResourceHandler>();
            services.TryAddScoped<IHandler<ApiScopeContract, int?>, ApiScopeHandler>();
            services.TryAddScoped<IHandler<IdentityResourceContract, int?>, IdentityResourceHandler>();
            services.TryAddScoped<IHandler<ClientsContract, int?>, ClientHandler>();
            services.TryAddScoped<IHandler<RolesContract, string>, RolesHandler>();
            services.TryAddScoped<IHandler<UserContract, string>, UserHandler>();

            //validators
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
    }
}
