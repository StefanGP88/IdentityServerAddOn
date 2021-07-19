using FluentValidation;
using FluentValidation.TestHelper;
using Ids.SimpleAdmin.Contracts;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.ContractBuilders;
using Xunit;

namespace UnitTests.ValidatorTests
{
    public class RedirectUrisValidatortests : TestBase<ClientRedirectUriContract>
    {
        public RedirectUrisValidatortests()
        {
            ContractBuilder = new ContractBuilder<ClientRedirectUriContract>(new ClientRedirectUriContract
            {
                RedirectUri = "HTTP://LOCALHOST:5000"
            });
        }

        [Fact]
        public void Test_Rules_For_RedirectUri_Property()
        {
            var validator = Provider.GetRequiredService<IValidator<ClientRedirectUriContract>>();

            // Assert that there should NOT be a failure for the RedirectUri property.
            var okRedirectUri = new string('a', Random.Next(2, 1999));
            var contract_ok = ContractBuilder.With(x => x.RedirectUri = okRedirectUri).Build();
            var result = validator.TestValidate(contract_ok);
            result.ShouldNotHaveValidationErrorFor(x => x.RedirectUri);

            var contract_ok_min = ContractBuilder.With(x => x.RedirectUri = "a").Build();
            result = validator.TestValidate(contract_ok_min);
            result.ShouldNotHaveValidationErrorFor(x => x.RedirectUri);

            var maxRedirectUri = new string('a', 2000);
            var contract_ok_max = ContractBuilder.With(x => x.RedirectUri = maxRedirectUri).Build();
            result = validator.TestValidate(contract_ok_max);
            result.ShouldNotHaveValidationErrorFor(x => x.RedirectUri);

            // Assert that there should be a failure for the RedirectUri property.
            var contract_short = ContractBuilder.With(x => x.RedirectUri = string.Empty).Build();
            result = validator.TestValidate(contract_short);
            result.ShouldHaveValidationErrorFor(x => x.RedirectUri);
            var longRedirectUri = new string('a', 2001);
            var contract_long = ContractBuilder.With(x => x.RedirectUri = longRedirectUri).Build();
            result = validator.TestValidate(contract_long);
            result.ShouldHaveValidationErrorFor(x => x.RedirectUri);

            var contract_null = ContractBuilder.With(x => x.RedirectUri = null).Build();
            result = validator.TestValidate(contract_null);
            result.ShouldHaveValidationErrorFor(x => x.RedirectUri);
        }
    }
}
