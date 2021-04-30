using IdentityServer.Data;
using Ids.SimpleAdmin.Backend;
using Ids.SimpleAdmin.Frontend;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Default");
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityContext(connectionString, migrationAssembly);

            services
                .AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            services
                .AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(connectionString, migrationAssembly)
                .AddOperationalStore(connectionString, migrationAssembly);

            services.AddRazorPages().AddSimpleAdmin();
            services.AddTransient<DemystifyMiddleWare>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            app.UseMiddleware<DemystifyMiddleWare>();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSimpleAdmin();
            app.UseEndpoints(endpoints => endpoints.MapRazorPages());
            EnsureMigrations(app);
        }

        private void EnsureMigrations(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var identityContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            identityContext.Database.Migrate();

            var IdsCfgContext = serviceScope.ServiceProvider.GetRequiredService<IdentityServer4.EntityFramework.DbContexts.ConfigurationDbContext>();
            IdsCfgContext.Database.Migrate();

            var IdsPgContext = serviceScope.ServiceProvider.GetRequiredService<IdentityServer4.EntityFramework.DbContexts.PersistedGrantDbContext>();
            IdsPgContext.Database.Migrate();
        }
    }

    public class DemystifyMiddleWare : IMiddleware
    {
        private readonly ILogger<DemystifyMiddleWare> _log;
        public DemystifyMiddleWare(ILogger<DemystifyMiddleWare> logger)
        {
            _log = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context).ConfigureAwait(false);

            }
            catch(Exception e)
            {
                _log.LogError(e, "orginal");
                e.Demystify();
                _log.LogError(e, "demystified");

                throw;
            }
        }
    }
}
