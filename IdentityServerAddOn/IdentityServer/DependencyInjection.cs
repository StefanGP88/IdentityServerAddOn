using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityContext(this IServiceCollection serviceCollection, string connectionString, string migrationAssembly)
        {
            return serviceCollection
                .AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly)));
        }
        public static IIdentityServerBuilder AddConfigurationStore(this IIdentityServerBuilder builder, string connectionString, string migrationAssembly)
        {
            return builder.AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
            });
        }
        public static IIdentityServerBuilder AddOperationalStore(this IIdentityServerBuilder builder, string connectionString, string migrationAssembly)
        {
            return builder.AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationAssembly));
                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 30;
            });
        }
    }
}
