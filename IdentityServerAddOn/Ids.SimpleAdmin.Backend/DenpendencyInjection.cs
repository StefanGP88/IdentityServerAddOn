using Microsoft.Extensions.DependencyInjection;
using Ids.SimpleAdmin.Backend.Interfaces;

namespace Ids.SimpleAdmin.Backend
{
    public static class DenpendencyInjection
    {
        public static void AddIdentityServerAddOn(this IServiceCollection services)
        {
            services.AddScoped<IRoleHandler, RoleHandler>();
        }
    }
}
