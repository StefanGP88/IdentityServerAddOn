using FluentValidation;
using FluentValidation.TestHelper;
using Ids.SimpleAdmin.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using UnitTests.ContractBuilders;
using Xunit;

namespace UnitTests.ValidatorTests
{
    public class ClaimsValidatorTests : TestBase<ClaimsContract>
    {
        public ClaimsValidatorTests()
        {
            ContractBuilder = new ContractBuilder<ClaimsContract>(new ClaimsContract
            {
                Type = "Super claim"
            });
        }


        [Fact]
        public void Test_Rules_For_Type_Property()
        {
            var validator = Provider.GetRequiredService<IValidator<ClaimsContract>>();

            // Assert that there should NOT be a failure for the Type property.
            var okDescription = new string('a', new Random().Next(1, 199));
            var contract_ok = ContractBuilder.With(x => x.Type = okDescription).Build();
            var result = validator.TestValidate(contract_ok);
            result.ShouldNotHaveValidationErrorFor(x => x.Type);

            var contract_ok_min = ContractBuilder.With(x => x.Type = string.Empty).Build();
            result = validator.TestValidate(contract_ok_min);
            result.ShouldNotHaveValidationErrorFor(x => x.Type);

            var maxDescription = new string('a', 200);
            var contract_ok_max = ContractBuilder.With(x => x.Type = maxDescription).Build();
            result = validator.TestValidate(contract_ok_max);
            result.ShouldNotHaveValidationErrorFor(x => x.Type);


            // Assert that there should be a failure for the Type property.
            var contract_null = ContractBuilder.With(x => x.Type = null).Build();
            result = validator.TestValidate(contract_null);
            result.ShouldHaveValidationErrorFor(x => x.Type);

            var longDescription = new string('a', 201);
            var contract_long = ContractBuilder.With(x => x.Type = longDescription).Build();
            result = validator.TestValidate(contract_long);
            result.ShouldHaveValidationErrorFor(x => x.Type);
        }

    }
}
