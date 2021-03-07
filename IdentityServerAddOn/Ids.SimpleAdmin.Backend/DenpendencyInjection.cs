using Microsoft.Extensions.DependencyInjection;
using Ids.SimpleAdmin.Backend.Handlers.Interfaces;
using Ids.SimpleAdmin.Backend.Handlers;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Backend
{
    public static class DenpendencyInjection
    {
        public static void AddIdentityServerAddOn(this IServiceCollection sc)
        {
            //TODO: make scoped
            sc.AddScoped<IHandler<ApiResourceContract, int?>, ApiResourceHandler>();
        }
    }
}
