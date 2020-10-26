using Microsoft.Extensions.DependencyInjection;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Handlers;

namespace Ids.SimpleAdmin.Backend
{
    public static class DenpendencyInjection
    {
        public static void AddIdentityServerAddOn(this IServiceCollection sc)
        {
            sc.AddScoped<IRoleHandler, RoleHandler>();
            sc.AddScoped<IUserHandler, UserHandler>();
            sc.AddScoped<IUserRoleHandler, UserRoleHandler>();

            //TODO: can these claim handlers user same interface ?
            sc.AddScoped<IRoleClaimHandler, RoleClaimHandler>();
            sc.AddScoped<IUserClaimHandler, UserClaimHandler>();
        }
    }
}
