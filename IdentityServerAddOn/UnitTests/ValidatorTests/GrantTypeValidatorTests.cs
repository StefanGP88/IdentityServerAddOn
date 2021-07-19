using FluentValidation;
using FluentValidation.TestHelper;
using Ids.SimpleAdmin.Contracts;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.ContractBuilders;
using Xunit;

namespace UnitTests.ValidatorTests
{
    public class GrantTypeValidatorTests : TestBase<ClientGrantTypesContract>
    {
        public GrantTypeValidatorTests()
        {
            ContractBuilder = new ContractBuilder<ClientGrantTypesContract>(new ClientGrantTypesContract
            {
                GrantType = "Huge Grant"
            });
        }

        [Fact]
        public void Test_Rules_For_GrantType_Property()
        {
            var validator = Provider.GetRequiredService<IValidator<ClientGrantTypesContract>>();

            // Assert that there should NOT be a failure for the GrantType property.
            var okGrantType = new string('a', Random.Next(2, 249));
            var contract_ok = ContractBuilder.With(x => x.GrantType = okGrantType).Build();
            var result = validator.TestValidate(contract_ok);
            result.ShouldNotHaveValidationErrorFor(x => x.GrantType);

            var contract_ok_min = ContractBuilder.With(x => x.GrantType = "a").Build();
            result = validator.TestValidate(contract_ok_min);
            result.ShouldNotHaveValidationErrorFor(x => x.GrantType);

            var maxGrantType = new string('a', 250);
            var contract_ok_max = ContractBuilder.With(x => x.GrantType = maxGrantType).Build();
            result = validator.TestValidate(contract_ok_max);
            result.ShouldNotHaveValidationErrorFor(x => x.GrantType);

            // Assert that there should be a failure for the GrantType property.
            var contract_short = ContractBuilder.With(x => x.GrantType = string.Empty).Build();
            result = validator.TestValidate(contract_short);
            result.ShouldHaveValidationErrorFor(x => x.GrantType);

            var longGrantType = new string('a', 251);
            var contract_long = ContractBuilder.With(x => x.GrantType = longGrantType).Build();
            result = validator.TestValidate(contract_long);
            result.ShouldHaveValidationErrorFor(x => x.GrantType);

            var contract_null = ContractBuilder.With(x => x.GrantType = null).Build();
            result = validator.TestValidate(contract_null);
            result.ShouldHaveValidationErrorFor(x => x.GrantType);
        }
    }
}
