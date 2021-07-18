using Ids.SimpleAdmin.Backend;
using Microsoft.Extensions.DependencyInjection;
using System;
using UnitTests.ContractBuilders;

namespace UnitTests.ValidatorTests
{
    public class TestBase<T>
    {
        public IServiceCollection Services { get; set; }
        public IServiceProvider Provider { get; set; }
        public ContractBuilder<T> ContractBuilder { get; set; }
        public TestBase()
        {
            Services = new ServiceCollection();
            Services.AddSimpleAdminBackend();
            Provider = Services.BuildServiceProvider();
        }
    }
}
