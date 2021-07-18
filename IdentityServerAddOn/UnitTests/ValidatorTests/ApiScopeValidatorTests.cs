using FluentValidation;
using FluentValidation.TestHelper;
using Ids.SimpleAdmin.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using UnitTests.ContractBuilders;
using Xunit;

namespace UnitTests.ValidatorTests
{
    public class ApiScopeValidatorTests : TestBase<ApiScopeContract>
    {
        public ApiScopeValidatorTests()
        {
            var defaultModel = new ApiScopeContract
            {
                Description = "Test api resource description",
                Claims = new List<ClaimsContract>(),
                DisplayName = "Test api resource",
                Enabled = true,
                Name = "Test api resource",
                Properties = new List<PropertyContract>(),
                ShowInDiscoveryDocument = true,
                Emphasize = false,
                Required = true,
            };
            ContractBuilder = new ContractBuilder<ApiScopeContract>(defaultModel);
        }
        [Fact]
        public void Test_Rules_For_Description_Property()
        {
            var validator = Provider.GetRequiredService<IValidator<ApiScopeContract>>();

            // Assert that there should NOT be a failure for the Description property.
            var okDescription = new string('a', new Random().Next(1, 999));
            var contract_ok = ContractBuilder.With(x => x.Description = okDescription).Build();
            var result = validator.TestValidate(contract_ok);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);

            var contract_ok_min = ContractBuilder.With(x => x.Description = string.Empty).Build();
            result = validator.TestValidate(contract_ok_min);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);

            var maxDescription = new string('a', 1000);
            var contract_ok_max = ContractBuilder.With(x => x.Description = maxDescription).Build();
            result = validator.TestValidate(contract_ok_max);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);

            var contract_null = ContractBuilder.With(x => x.Description = null).Build();
            result = validator.TestValidate(contract_null);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);

            var contract_short = ContractBuilder.With(x => x.Description = string.Empty).Build();
            result = validator.TestValidate(contract_short);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);

            // Assert that there should be a failure for the Description property.
            var longDescription = new string('a', 1001);
            var contract_long = ContractBuilder.With(x => x.Description = longDescription).Build();
            result = validator.TestValidate(contract_long);
            result.ShouldHaveValidationErrorFor(x => x.Description);

        }
    }
}
