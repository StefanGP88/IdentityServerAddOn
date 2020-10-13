using IdentityServer;
using Ids.SimpleAdmin.Backend;

namespace Ids.SimpleAdmin.Tests
{
    public class RoleFixture : IdentityFixture
    {
        public RoleFixture()
        {
            var connectionsString = GetConnectionString(GetType().Name);
            var migrationAssembly = GetMigrationAssembly();
            Setup(services =>
            {
                services.AddIdentityServerAddOn();
                services.AddIdentityContext(connectionsString, migrationAssembly);
            }, null);
        }
    }
}
