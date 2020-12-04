using Microsoft.Extensions.DependencyInjection;
using Ids.SimpleAdmin.Backend;
using System.Reflection;
using Microsoft.AspNetCore.Builder;

namespace RazorTestLibrary
{
    public static class DependencyInjection
    {
        private static void AddSimpleAdminDependencyInjection(this IServiceCollection services)
        {
            services.AddIdentityServerAddOn();
            services.AddScoped<PageSizeMiddleware>();
        }
        public static IMvcBuilder AddRazorPagesForSimpleAdmin(this IMvcBuilder builder)
        {
            builder.Services.AddSimpleAdminDependencyInjection();
            var assembly = Assembly.GetExecutingAssembly().GetName().Name;
            builder.AddApplicationPart(Assembly.Load(assembly));
            
            
            return builder;
        }

        public static IApplicationBuilder UseSimpleAdmin(this IApplicationBuilder app)
        {
            app.UseMiddleware<PageSizeMiddleware>();
            return app;
        }
    }
}
