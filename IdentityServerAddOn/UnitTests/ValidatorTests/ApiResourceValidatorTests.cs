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
    public class ApiResourceValidatorTests: TestBase<ApiResourceContract>
    {
        public ApiResourceValidatorTests()
        {
            var defaultModel = new ApiResourceContract
            {
                AllowedAccessTokenSigningAlgorithms = "",
                Created = DateTime.UtcNow,
                Description = "Test api resource description",
                Claims = new List<ClaimsContract>(),
                DisplayName = "Test api resource",
                Enabled = true,
                LastAccessed = DateTime.UtcNow,
                Name = "Test api resource",
                NonEditable = true,
                Properties = new List<PropertyContract>(),
                ShowInDiscoveryDocument = true,
                Scopes = new List<ScopeContract>(),
                Secrets = new List<ApiResourceSecretsContract>(),
                Updated = DateTime.UtcNow
            };
            ContractBuilder = new ContractBuilder<ApiResourceContract>(defaultModel);
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

            var contract_ok_min = ContractBuilder.With(x => x.Name = "a").Build();
            result = validator.TestValidate(contract_ok_min);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);

            var maxName = new string('a', 200);
            var contract_ok_max = ContractBuilder.With(x => x.Name = maxName).Build();
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

        [Fact]
        public void Test_Rules_For_DisplayName_Property()
        {
            var validator = Provider.GetRequiredService<IValidator<ApiResourceContract>>();

            // Assert that there should NOT be a failure for the DisplayName property.
            var okDisplayName = new string('a', new Random().Next(2, 199));
            var contract_ok = ContractBuilder.With(x => x.DisplayName = okDisplayName).Build();
            var result = validator.TestValidate(contract_ok);
            result.ShouldNotHaveValidationErrorFor(x => x.DisplayName);

            var contract_ok_min = ContractBuilder.With(x => x.DisplayName = "a").Build();
            result = validator.TestValidate(contract_ok_min);
            result.ShouldNotHaveValidationErrorFor(x => x.DisplayName);

            var maxDisplayName = new string('a', 200);
            var contract_ok_max = ContractBuilder.With(x => x.DisplayName = maxDisplayName).Build();
            result = validator.TestValidate(contract_ok_max);
            result.ShouldNotHaveValidationErrorFor(x => x.DisplayName);

            // Assert that there should be a failure for the DisplayName property.
            var contract_short = ContractBuilder.With(x => x.DisplayName = string.Empty).Build();
            result = validator.TestValidate(contract_short);
            result.ShouldHaveValidationErrorFor(x => x.DisplayName);

            var longDisplayName = new string('a', 201);
            var contract_long = ContractBuilder.With(x => x.DisplayName = longDisplayName).Build();
            result = validator.TestValidate(contract_long);
            result.ShouldHaveValidationErrorFor(x => x.DisplayName);

            var contract_null = ContractBuilder.With(x => x.DisplayName = null).Build();
            result = validator.TestValidate(contract_null);
            result.ShouldHaveValidationErrorFor(x => x.DisplayName);
        }

        [Fact]
        public void Test_Rules_For_Description_Property()
        {
            var validator = Provider.GetRequiredService<IValidator<ApiResourceContract>>();

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

        [Fact]
        public void Test_Rules_For_AllowedAccessTokenSigningAlgorithms_Property()
        {
            var validator = Provider.GetRequiredService<IValidator<ApiResourceContract>>();

            // Assert that there should NOT be a failure for the AllowedAccessTokenSigningAlgorithms property.
            var okAllowedAccessTokenSigningAlgorithms = new string('a', new Random().Next(1, 99));
            var contract_ok = ContractBuilder
                .With(x => x.AllowedAccessTokenSigningAlgorithms = okAllowedAccessTokenSigningAlgorithms)
                .Build();
            var result = validator.TestValidate(contract_ok);
            result.ShouldNotHaveValidationErrorFor(x => x.AllowedAccessTokenSigningAlgorithms);

            var contract_ok_min = ContractBuilder
                .With(x => x.AllowedAccessTokenSigningAlgorithms = string.Empty)
                .Build();
            result = validator.TestValidate(contract_ok_min);
            result.ShouldNotHaveValidationErrorFor(x => x.AllowedAccessTokenSigningAlgorithms);

            var maxAllowedAccessTokenSigningAlgorithms = new string('a', 100);
            var contract_ok_max = ContractBuilder
                .With(x => x.AllowedAccessTokenSigningAlgorithms = maxAllowedAccessTokenSigningAlgorithms)
                .Build();
            result = validator.TestValidate(contract_ok_max);
            result.ShouldNotHaveValidationErrorFor(x => x.AllowedAccessTokenSigningAlgorithms);

            var contract_null = ContractBuilder
                .With(x => x.AllowedAccessTokenSigningAlgorithms = null)
                .Build();
            result = validator.TestValidate(contract_null);
            result.ShouldNotHaveValidationErrorFor(x => x.AllowedAccessTokenSigningAlgorithms);

            var contract_short = ContractBuilder
                .With(x => x.AllowedAccessTokenSigningAlgorithms = string.Empty)
                .Build();
            result = validator.TestValidate(contract_short);
            result.ShouldNotHaveValidationErrorFor(x => x.AllowedAccessTokenSigningAlgorithms);

            // Assert that there should be a failure for the AllowedAccessTokenSigningAlgorithms property.

            var longAllowedAccessTokenSigningAlgorithms = new string('a', 201);
            var contract_long = ContractBuilder
                .With(x => x.AllowedAccessTokenSigningAlgorithms = longAllowedAccessTokenSigningAlgorithms)
                .Build();
            result = validator.TestValidate(contract_long);
            result.ShouldHaveValidationErrorFor(x => x.AllowedAccessTokenSigningAlgorithms);

        }
    }
}
