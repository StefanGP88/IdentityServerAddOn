using FluentValidation;
using FluentValidation.TestHelper;
using Ids.SimpleAdmin.Contracts;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.ContractBuilders;
using Xunit;

namespace UnitTests.ValidatorTests
{
    public class PostLogoutRedirectUrisValidatorTests : TestBase<ClientPostLogoutRedirectUrisContract>
    {
        public PostLogoutRedirectUrisValidatorTests()
        {
            ContractBuilder = new ContractBuilder<ClientPostLogoutRedirectUrisContract>(new ClientPostLogoutRedirectUrisContract
            {
                PostLogoutRedirectUri = "http://localhost:5000"
            });
        }
        [Fact]
        public void Test_Rules_For_PostLogoutRedirectUri_Property()
        {
            ForProperty(x => x.PostLogoutRedirectUri);
            var validator = Provider.GetRequiredService<IValidator<ClientPostLogoutRedirectUrisContract>>();

            // Assert that there should NOT be a failure for the PostLogoutRedirectUri property.
            var okPostLogoutRedirectUri = new string('a', Random.Next(2, 1999));
            var contract_ok = ContractBuilder.With(x => x.PostLogoutRedirectUri = okPostLogoutRedirectUri).Build();

            var result = validator.TestValidate(contract_ok);
            result.ShouldNotHaveValidationErrorFor(x => x.PostLogoutRedirectUri);

            var contract_ok_min = ContractBuilder.With(x => x.PostLogoutRedirectUri = "a").Build();
            result = validator.TestValidate(contract_ok_min);
            result.ShouldNotHaveValidationErrorFor(x => x.PostLogoutRedirectUri);

            var maxPostLogoutRedirectUri = new string('a', 2000);
            var contract_ok_max = ContractBuilder.With(x => x.PostLogoutRedirectUri = maxPostLogoutRedirectUri).Build();
            result = validator.TestValidate(contract_ok_max);
            result.ShouldNotHaveValidationErrorFor(x => x.PostLogoutRedirectUri);

            // Assert that there should be a failure for the PostLogoutRedirectUri property.
            var contract_short = ContractBuilder.With(x => x.PostLogoutRedirectUri = string.Empty).Build();
            result = validator.TestValidate(contract_short);
            result.ShouldHaveValidationErrorFor(x => x.PostLogoutRedirectUri);

            var longPostLogoutRedirectUri = new string('a', 2001);
            var contract_long = ContractBuilder.With(x => x.PostLogoutRedirectUri = longPostLogoutRedirectUri).Build();
            result = validator.TestValidate(contract_long);
            result.ShouldHaveValidationErrorFor(x => x.PostLogoutRedirectUri);

            var contract_null = ContractBuilder.With(x => x.PostLogoutRedirectUri = null).Build();
            result = validator.TestValidate(contract_null);
            result.ShouldHaveValidationErrorFor(x => x.PostLogoutRedirectUri);
        }
    }
}