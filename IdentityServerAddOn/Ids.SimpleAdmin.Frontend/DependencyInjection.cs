using FluentValidation.AspNetCore;
using Ids.SimpleAdmin.Backend;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
            var assembly = Assembly.GetExecutingAssembly().GetName().Name;
            builder.AddApplicationPart(Assembly.Load(assembly));

            /*************************/
            //TODO: MOVE TO SOMEWHERE MORE SENSIBLE
            //TODO TODO REWRITE TO NOT KNOW APPDBCONTEXT NAME
            var entryAsm = Assembly.GetEntryAssembly(); //can this be called from anywhere and still get Identityserver project
            //var callingAssembly = Assembly.GetCallingAssembly();

            var scEnumirator = builder.Services.GetEnumerator();
            while (scEnumirator.MoveNext())
            {
                var current = scEnumirator.Current;
                var serviceType = current.ServiceType;

                if (serviceType.BaseType is not null)
                {
                    if (serviceType.Name == "AppDbContext")
                    {
                        //var appDbCtx = callingAssembly.GetType(serviceType.FullName);
                        var appDbCtx = entryAsm.GetType(serviceType.FullName);
                        System.Console.WriteLine(appDbCtx);
                        var baseDbCtx = appDbCtx.BaseType;

                        builder.Services.AddScoped(baseDbCtx, appDbCtx);

                        var abc = 2; //this is where we can find adcontext type
                        break;
                    }
                    else
                    {
                        System.Console.WriteLine(serviceType.Name);
                    }
                }


            }
            /*********************************************/


            return builder;
        }

        public static IApplicationBuilder UseSimpleAdmin(this IApplicationBuilder app)
        {
            app.UseMiddleware<PageSizeMiddleware>();
            return app;
        }
    }
}
