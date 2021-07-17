using Ids.SimpleAdmin.Contracts;
using System;
using System.Collections.Generic;

namespace UnitTests.ContractBuilders
{
    public class ApiResourceContractBuilder
    {
        public ApiResourceContractBuilder()
        {
            Default = new ApiResourceContract
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
            Current = Default;
        }

        private ApiResourceContract Default;
        private ApiResourceContract Current;

        public ApiResourceContractBuilder With(Action<ApiResourceContract> action)
        {
            action(Current);
            return this;
        }

        public ApiResourceContract Build()
        {
            var current = Current;
            Current = Default;
            return current;
        }

    }
}
