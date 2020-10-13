using IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Ids.SimpleAdmin.Tests
{
    public class IdentityFixture : IDisposable
    {
        private DbContext DbContext { get; set; }
        private IServiceCollection Services { get; set; }
        private ServiceProvider ServiceProvider { get; set; }
        public void Setup(Action<IServiceCollection> services, Action<ServiceProvider> serviceProvider)
        {
            SetServiceProvider(services, serviceProvider);
            CreateTestDb();
        }
        public string GetConnectionString(string dbName)
        {
            dbName = dbName.Replace("Fixture", "").Replace("fixture", "");
            return $"Data Source=.;Initial Catalog={dbName};Integrated Security = true;MultipleActiveResultSets=True;";
        }
        public string GetMigrationAssembly()
        {
            return typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
        }
        private void SetServiceCollections(Action<IServiceCollection> services)
        {
            Services = new ServiceCollection();
            services?.Invoke(Services);
        }
        private void SetServiceProvider(Action<IServiceCollection> services, Action<ServiceProvider> serviceProvider)
        {
            SetServiceCollections(services);
            ServiceProvider = Services.BuildServiceProvider();
            serviceProvider?.Invoke(ServiceProvider);
        }
        private void CreateTestDb()
        {
            DbContext = ServiceProvider.GetRequiredService<DbContext>();
            DbContext.Database.EnsureDeleted();
            DbContext.Database.Migrate();
        }

        #region dispose

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~IdentityFixture()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion dispose
    }
}
