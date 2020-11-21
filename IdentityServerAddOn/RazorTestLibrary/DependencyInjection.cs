using Microsoft.Extensions.DependencyInjection;
using Ids.SimpleAdmin.Backend;
using System.Reflection;

namespace RazorTestLibrary
{
    public static class DependencyInjection
    {
        private static void AddSimpleAdminDependencyInjection(this IServiceCollection services)
        {
            services.AddIdentityServerAddOn();
        }
        public static IMvcBuilder AddRazorPagesForSimpleAdmin(this IMvcBuilder builder)
        {
            builder.Services.AddSimpleAdminDependencyInjection();

            var assembly = Assembly.GetExecutingAssembly().GetName().Name;
            builder.AddApplicationPart(Assembly.Load(assembly));
            //builder.AddRazorOptions(x =>
            //{
            //    x.AreaPageViewLocationFormats.Add("/PartialViews/{0}.cshtml");
            //    x.ViewLocationFormats.Add("/PartialViews/{0}.cshtml");
            //});

            return builder;
        }
    }
}
