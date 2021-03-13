using Microsoft.Extensions.DependencyInjection;
using Ids.SimpleAdmin.Backend;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using Ids.SimpleAdmin.Backend.Validators;
using Ids.SimpleAdmin.Contracts;

namespace Ids.SimpleAdmin.Frontend
{
    public static class DependencyInjection
    {
        private static void AddSimpleAdminDependencyInjection(this IServiceCollection services)
        {
            services.AddIdentityServerAddOn();
            services.AddScoped<PageSizeMiddleware>();

            services.AddHttpContextAccessor();
        }
        public static IMvcBuilder AddRazorPagesForSimpleAdmin(this IMvcBuilder builder)
        {
            builder.Services.AddSimpleAdminDependencyInjection();
            builder.AddFluentValidation();
            builder.Services.AddTransient<IValidator<ApiResourceContract>, ApiResourceValidator>();
            builder.Services.AddTransient<IValidator<ApiResourceClaimsContract>, ApiResourceClaimsValidator>();
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
