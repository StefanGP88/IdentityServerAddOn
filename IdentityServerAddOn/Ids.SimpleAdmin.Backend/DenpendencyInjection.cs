using Microsoft.Extensions.DependencyInjection;
using Ids.SimpleAdmin.Backend.Interfaces;

namespace Ids.SimpleAdmin.Backend
{
    public static class DenpendencyInjection
    {
        public static void AddIdentityServerAddOn(this IServiceCollection sc)
        {
            sc.AddScoped<IRoleHandler, RoleHandler>();
            sc.AddScoped<IUserHandler, UserHandler>();
            sc.AddScoped<IUserRoleHandler, UserRoleHandler>();
        }
    }
}
