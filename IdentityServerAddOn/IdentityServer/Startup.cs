using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Data;
using Ids.SimpleAdmin.Backend;
using RazorTestLibrary;

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



            //services
            //    .AddIdentityServer()
            //    .AddAspNetIdentity<IdentityUser>()
            //    .AddConfigurationStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //            builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
            //    })
            //    .AddOperationalStore(options =>
            //    {
            //        options.ConfigureDbContext = builder =>
            //            builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
            //        options.EnableTokenCleanup = true;
            //        options.TokenCleanupInterval = 30;
            //    });

            services
                .AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(connectionString, migrationAssembly)
                .AddOperationalStore(connectionString, migrationAssembly);

            services.AddRazorPages().AddRazorPagesForSimpleAdmin<AppDbContext>();

            services.AddIdentityServerAddOn<AppDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
}
