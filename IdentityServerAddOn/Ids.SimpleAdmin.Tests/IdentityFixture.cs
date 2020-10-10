using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ids.SimpleAdmin.Tests
{
    public class IdentityFixture
    {
        private DbContext dbContext { get; set; }

        private IServiceCollection Services { get; set; }

        private ServiceProvider ServiceProvider { get; set; }
    }
}
