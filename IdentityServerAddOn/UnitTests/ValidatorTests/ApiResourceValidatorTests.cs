using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using Ids.SimpleAdmin.Backend;
using Ids.SimpleAdmin.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace UnitTests.ValidatorTests
{
    public class ApiResourceValidatorTests
    {
        IServiceCollection Services { get; set; }
        IServiceProvider Provider { get; set; }
        public ApiResourceValidatorTests()
        {
            Services = new ServiceCollection();
            Services.AddSimpleAdminBackend();
            Provider = Services.BuildServiceProvider();
        }
        [Fact]
        public void Hallo_world()
        {
            var validator = Provider.GetRequiredService<IValidator<ApiResourceContract>>();
            var result = validator.TestValidate(new ApiResourceContract());

            //should NOT have error
            result.ShouldNotHaveValidationErrorFor(x => x.Name);


            // Assert that there should be a failure for the Name property.
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
