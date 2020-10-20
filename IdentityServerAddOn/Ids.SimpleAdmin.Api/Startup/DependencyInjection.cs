using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ids.SimpleAdmin.Api.Startup
{
    public static class DenpendencyInjection
    {
        public static void AddIdentityServerAddOn(this IServiceCollection services)
        {
            services.AddMvc().AddSimpleAdminControllers();
        }
        public static IMvcBuilder AddSimpleAdminControllers(this IMvcBuilder builder)
        {
            var assembly = Assembly.GetExecutingAssembly().GetName().Name;
            return builder.AddApplicationPart(Assembly.Load(assembly));
        }
    }
}
