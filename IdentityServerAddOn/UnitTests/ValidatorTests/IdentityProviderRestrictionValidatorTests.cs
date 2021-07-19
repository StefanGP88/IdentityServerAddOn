using FluentValidation;
using FluentValidation.TestHelper;
using Ids.SimpleAdmin.Contracts;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.ContractBuilders;
using Xunit;

namespace UnitTests.ValidatorTests
{
    public class IdentityProviderRestrictionValidatorTests:TestBase<ClientIdPRestrictionsContract>
    {
        public IdentityProviderRestrictionValidatorTests()
        {
            ContractBuilder = new ContractBuilder<ClientIdPRestrictionsContract>(new ClientIdPRestrictionsContract
            {
                Provider = "Some test provider"
            });
        }

        [Fact]
        public void Test_Rules_For_Provider_Property()
        {
            var validator = Provider.GetRequiredService<IValidator<ClientIdPRestrictionsContract>>();

            // Assert that there should NOT be a failure for the Provider property.
            var okProvider = new string('a', Random.Next(2, 199));
            var contract_ok = ContractBuilder.With(x => x.Provider = okProvider).Build();
            var result = validator.TestValidate(contract_ok);
            result.ShouldNotHaveValidationErrorFor(x => x.Provider);

            var contract_ok_min = ContractBuilder.With(x => x.Provider = "a").Build();
            result = validator.TestValidate(contract_ok_min);
            result.ShouldNotHaveValidationErrorFor(x => x.Provider);

            var maxProvider = new string('a', 200);
            var contract_ok_max = ContractBuilder.With(x => x.Provider = maxProvider).Build();
            result = validator.TestValidate(contract_ok_max);
            result.ShouldNotHaveValidationErrorFor(x => x.Provider);

            // Assert that there should be a failure for the Provider property.
            var contract_short = ContractBuilder.With(x => x.Provider = string.Empty).Build();
            result = validator.TestValidate(contract_short);
            result.ShouldHaveValidationErrorFor(x => x.Provider);

            var longProvider = new string('a', 201);
            var contract_long = ContractBuilder.With(x => x.Provider = longProvider).Build();
            result = validator.TestValidate(contract_long);
            result.ShouldHaveValidationErrorFor(x => x.Provider);

            var contract_null = ContractBuilder.With(x => x.Provider = null).Build();
            result = validator.TestValidate(contract_null);
            result.ShouldHaveValidationErrorFor(x => x.Provider);
        }
    }
}
