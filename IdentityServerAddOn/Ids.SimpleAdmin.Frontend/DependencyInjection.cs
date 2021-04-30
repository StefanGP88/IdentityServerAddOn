using FluentValidation.AspNetCore;
using Ids.SimpleAdmin.Backend;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;


namespace Ids.SimpleAdmin.Frontend
{
    public static class DependencyInjection
    {
        private static void AddSimpleAdminDependencyInjection(this IServiceCollection services)
        {
            services.AddSimpleAdminBackend();
            services.AddScoped<PageSizeMiddleware>();
            services.AddHttpContextAccessor();
        }
        public static IMvcBuilder AddSimpleAdmin(this IMvcBuilder builder)
        {
            
            builder.Services.AddSimpleAdminBackend();
            
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
                        //System.Console.WriteLine(appDbCtx);
                        var baseDbCtx = appDbCtx.BaseType;

                        builder.Services.AddScoped(baseDbCtx, appDbCtx);

                        var abc = 2; //this is where we can find adcontext type
                        break;
                    }
                    else
                    {
                        //System.Console.WriteLine(serviceType.Name);
                    }
                }


            }
            /*********************************************/



            var scEnumirator2 = builder.Services.GetEnumerator();


            while (scEnumirator2.MoveNext())
            {
                var current = scEnumirator2.Current;
                var serviceType = current.ServiceType;
                var asm = Assembly.GetAssembly(serviceType);
                var appDbCtx = asm.GetType(serviceType.FullName);
                if (serviceType.FullName.Contains("store", StringComparison.OrdinalIgnoreCase))
                {
                    System.Console.WriteLine("!!!" +appDbCtx);
                }
                else
                {
                    System.Console.WriteLine(appDbCtx);
                }
                    UnpackBaseType(serviceType, "");
            }

            //https://stackoverflow.com/questions/32383613/prevent-asp-net-identitys-usermanager-from-automatically-saving
            //https://www.google.com/search?q=prevent+usermanager+to+autosavechanges&sxsrf=ALeKk02_Eu3WCnbppTovQaCLKrZ7xYP70g%3A1618517275684&source=hp&ei=G514YPTLJsjvkgW3jY-ICw&iflsig=AINFCbYAAAAAYHirK425eEyvaP-LRAZxW01fED48p2nQ&oq=prevent+usermanager+to+autosavechanges&gs_lcp=Cgdnd3Mtd2l6EAM6BAgjECc6AgguOgIIADoICC4QxwEQrwE6BQgAEMsBOgUIIRCgAToICCEQFhAdEB5QxglYx1tgn2hoAHAAeAGAAcICiAG3GZIBCTIxLjEwLjAuMZgBAKABAaoBB2d3cy13aXo&sclient=gws-wiz&ved=0ahUKEwi0xZvahoHwAhXIt6QKHbfGA7EQ4dUDCAc&uact=5

            var scEnumerator3 = builder.Services.GetEnumerator();
            while (scEnumerator3.MoveNext())
            {

                var current = scEnumerator3.Current;
                var serviceType = current.ServiceType;
                if(serviceType == typeof(UserManager<IdentityUser>))
                {
                    var initer = serviceType.GetInterfaces();
                    var properties = serviceType.GetProperties();
                    var abc = 0;
                }
                if(serviceType == typeof(IUserStore<IdentityUser>))
                {
                    var initer = serviceType.GetInterfaces();
                    var properties = serviceType.GetProperties();
                    var abc = 1;
                }
                if (serviceType == typeof(UserStore<IdentityUser>))
                {
                    var initer = serviceType.GetInterfaces();
                    var properties = serviceType.GetProperties();
                    var abc = 1;
                }
            }


            builder.Services.AddScoped(typeof(IUserStore<IdentityUser<string>>), typeof(UserStore<IdentityUser<string>>));
            builder.Services.AddScoped(typeof(DbContext), typeof(IdentityDbContext));
                
            return builder;
        }

        private static void UnpackBaseType(Type type, string layer)
        {
            if (type.BaseType is not null)
            {
                var bType = type.BaseType;
                var asm = Assembly.GetAssembly(bType);
                var appDbCtx = asm.GetType(bType.FullName);
                if (bType.FullName.Contains("store", StringComparison.OrdinalIgnoreCase))
                {
                    System.Console.WriteLine("!!!" + layer + appDbCtx);
                }
                else
                {
                    System.Console.WriteLine(layer + appDbCtx);
                }
                UnpackBaseType(bType, ">"+ layer);
            }
        }



        public static IApplicationBuilder UseSimpleAdmin(this IApplicationBuilder app)
        {
            app.UseMiddleware<PageSizeMiddleware>();
            return app;
        }

        private static TypeInfo FindGenericBaseType(Type currentType, Type genericBaseType)
        {
            var type = currentType;
            while (type != null)
            {
                var typeInfo = type.GetTypeInfo();
                var genericType = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
                if (genericType != null && genericType == genericBaseType)
                {
                    return typeInfo;
                }
                type = type.BaseType;
            }
            return null;
        }
    }
}
