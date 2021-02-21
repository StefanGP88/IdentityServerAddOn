using Microsoft.Extensions.DependencyInjection;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Handlers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Ids.SimpleAdmin.Backend.Dtos;

namespace Ids.SimpleAdmin.Backend
{
    public static class DenpendencyInjection
    {
        public static void AddIdentityServerAddOn<TContext>(this IServiceCollection sc) where TContext : IdentityDbContext
        {
            sc.AddScoped<IRoleHandler, RoleHandler>();
            sc.AddScoped<IUserHandler, UserHandler<TContext>>();
            sc.AddScoped<IApiScopeHandler, ApiScopeHandler>();

            sc.AddScoped<IUserRoleHandler, UserRoleHandler>();
            //sc.AddScoped<IApiResourceHandler, ApiResourceHandler>();
            sc.AddScoped<IHandler<ApiResourceResponseDto>, ApiResourceHandler>();

            //TODO: can these claim handlers user same interface ?
            sc.AddScoped<IRoleClaimHandler, RoleClaimHandler>();
            sc.AddScoped<IUserClaimHandler, UserClaimHandler>();
        }
    }
}
