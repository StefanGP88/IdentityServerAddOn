using Microsoft.Extensions.DependencyInjection;
using Ids.SimpleAdmin.Backend;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RazorTestLibrary
{
    public static class DependencyInjection
    {
        private static void AddSimpleAdminDependencyInjection<TContext>(this IServiceCollection services) where TContext : IdentityDbContext
        {
            services.AddIdentityServerAddOn<TContext>();
            services.AddScoped<PageSizeMiddleware>();
        }
        public static IMvcBuilder AddRazorPagesForSimpleAdmin<TContext>(this IMvcBuilder builder) where TContext : IdentityDbContext
        {
            builder.Services.AddSimpleAdminDependencyInjection<TContext>();
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
