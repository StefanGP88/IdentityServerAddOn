using FluentValidation;
using Ids.SimpleAdmin.Backend.Handlers;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Validators;
using Ids.SimpleAdmin.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

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
            sc.AddScoped<IHandler<RolesContract, string>, RolesHandler>();

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
            sc.AddTransient<IValidator<ClientCorsOriginsContract>, ClientCorsOriginsValidator>();
            sc.AddTransient<IValidator<ClientClaimsContract>, ClientClaimsValidator>();
            sc.AddTransient<IValidator<RolesContract>, RolesValidator>();
            sc.AddTransient<IValidator<RoleClaimsContract>, RoleClaimsValidator>();
            sc.AddTransient<IValidator<UserContract>, UserValidator>();
            sc.AddTransient<IValidator<UserClaimsContract>, UserClaimsValidator>();
            sc.AddTransient<ValidationFactory>();

            sc.AddScoped<TestManager>();
        }
    }
    public class TestManager : UserManager<IdentityUser>
    {
        public TestManager(IUserStore<IdentityUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<IdentityUser> passwordHasher,
            IEnumerable<IUserValidator<IdentityUser>> userValidators, IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<IdentityUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            var type = store.GetType();
            var autosave = type.GetProperty("AutoSaveChanges");
            autosave.SetValue(store, false);
        }
    }
}
