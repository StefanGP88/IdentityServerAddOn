using FluentValidation;
using FluentValidation.TestHelper;
using Ids.SimpleAdmin.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using UnitTests.ContractBuilders;
using Xunit;

namespace UnitTests.ValidatorTests
{
    public class CorsOriginValidatorTests : TestBase<ClientCorsOriginsContract>
    {
        public CorsOriginValidatorTests()
        {
            ContractBuilder = new ContractBuilder<ClientCorsOriginsContract>(new ClientCorsOriginsContract
            {
                Origin = "http://localhost:5000"
            });
        }
        [Fact]
        public void Test_Rules_For_Origin_Property()
        {
            var validator = Provider.GetRequiredService<IValidator<ClientCorsOriginsContract>>();

            // Assert that there should NOT be a failure for the Origin property.
            var okOrigin = new string('a', Random.Next(2, 149));
            var contract_ok = ContractBuilder.With(x => x.Origin = okOrigin).Build();
            var result = validator.TestValidate(contract_ok);
            result.ShouldNotHaveValidationErrorFor(x => x.Origin);


            var maxOrigin = new string('a', 150);
            var contract_ok_max = ContractBuilder.With(x => x.Origin = maxOrigin).Build();
            result = validator.TestValidate(contract_ok_max);
            result.ShouldNotHaveValidationErrorFor(x => x.Origin);

            var contract_min_ok = ContractBuilder.With(x => x.Origin = "a").Build();
            result = validator.TestValidate(contract_min_ok);
            result.ShouldNotHaveValidationErrorFor(x => x.Origin);

            // Assert that there should be a failure for the Origin property.
            var contract_null = ContractBuilder.With(x => x.Origin = null).Build();
            result = validator.TestValidate(contract_null);
            result.ShouldHaveValidationErrorFor(x => x.Origin);

            var longOrigin = new string('a', 151);
            var contract_long = ContractBuilder.With(x => x.Origin = longOrigin).Build();
            result = validator.TestValidate(contract_long);
            result.ShouldHaveValidationErrorFor(x => x.Origin);

        }
    }
}
