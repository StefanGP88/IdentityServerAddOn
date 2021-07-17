using FluentValidation;
using FluentValidation.TestHelper;
using Ids.SimpleAdmin.Backend;
using Ids.SimpleAdmin.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using UnitTests.ContractBuilders;
using Xunit;

namespace UnitTests.ValidatorTests
{
    public class ApiResourceValidatorTests
    {
        IServiceCollection Services { get; set; }
        IServiceProvider Provider { get; set; }
        ApiResourceContractBuilder ContractBuilder { get; set; }
        public ApiResourceValidatorTests()
        {
            ContractBuilder = new ApiResourceContractBuilder();
            Services = new ServiceCollection();
            Services.AddSimpleAdminBackend();
            Provider = Services.BuildServiceProvider();
        }
        [Fact]
        public void Test_Rules_For_Name_Property()
        {
            var validator = Provider.GetRequiredService<IValidator<ApiResourceContract>>();

            // Assert that there should NOT be a failure for the Name property.
            var okName = new string('a', new Random().Next(2, 199));
            var contract_ok = ContractBuilder.With(x => x.Name = okName).Build();
            var result = validator.TestValidate(contract_ok);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);

            var contract_ok_min = ContractBuilder.With(x => x.Name = okName).Build();
            result = validator.TestValidate(contract_ok_min);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);

            var maxName = new string('a', 200);
            var contract_ok_max = ContractBuilder.With(x => x.Name = okName).Build();
            result = validator.TestValidate(contract_ok_max);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);

            // Assert that there should be a failure for the Name property.
            var contract_short = ContractBuilder.With(x => x.Name = string.Empty).Build();
            result = validator.TestValidate(contract_short);
            result.ShouldHaveValidationErrorFor(x => x.Name);

            var longName = new string('a', 201);
            var contract_long = ContractBuilder.With(x => x.Name = longName).Build();
            result = validator.TestValidate(contract_long);
            result.ShouldHaveValidationErrorFor(x => x.Name);

            var contract_null = ContractBuilder.With(x => x.Name = null).Build();
            result = validator.TestValidate(contract_null);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
