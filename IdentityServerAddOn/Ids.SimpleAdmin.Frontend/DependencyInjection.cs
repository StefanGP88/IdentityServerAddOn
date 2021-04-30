using FluentValidation.AspNetCore;
using Ids.SimpleAdmin.Backend;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;


namespace Ids.SimpleAdmin.Frontend
{
    public static class DependencyInjection
    {
        public static void AddSimpleAdmin(this IMvcBuilder builder)
        {
            builder.Services.AddSimpleAdminBackend();
            builder.Services.AddScoped<PageSizeMiddleware>();
            builder.Services.AddHttpContextAccessor();
            builder.AddFluentValidation();
            
            var assembly = Assembly.GetExecutingAssembly().GetName().Name;
            builder.AddApplicationPart(Assembly.Load(assembly ?? throw new InvalidOperationException()));

            EnsureIdentityDbContextInjection(builder);
        }
        public static void UseSimpleAdmin(this IApplicationBuilder app)
        {
            app.UseMiddleware<PageSizeMiddleware>();
            app.UseEndpoints(endpoints => endpoints.MapRazorPages());
        }

        private static void EnsureIdentityDbContextInjection(IMvcBuilder builder)
        {
            var entryAsm = Assembly.GetEntryAssembly();
            using var enumerator = builder.Services.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                var serviceType = current?.ServiceType;

                if (serviceType?.BaseType is null) continue;
                if (serviceType.BaseType.Name != nameof(IdentityDbContext)) continue;
                
                var appDbCtx = entryAsm?.GetType(serviceType.FullName ?? serviceType.Name);
                var baseDbCtx = appDbCtx?.BaseType;
                builder.Services.AddScoped(baseDbCtx ?? throw new InvalidOperationException(), appDbCtx);
                break;
            }
        }
    }
}