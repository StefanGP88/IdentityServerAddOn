using System;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    //TODO: make sure validation is implemented
    public class ApiResourceContract
    {
        public int? Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string AllowedAccessTokenSigningAlgorithms { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime LastAccessed { get; set; }
        public bool NonEditable { get; set; }
        public List<ApiResourceClaimsContract> ApiResourceClaims { get; set; }
        public List<ApiResourcePropertiesContract> ApiResourceProperties { get; set; }
        public List<ApiResourceScopesContract> ApiResourceScopes { get; set; }
        public List<ApiResourceSecretsContract> ApiResourceSecrets { get; set; }
    }
    public class ApiResourceClaimsContract
    {
        public int? Id { get; set; }
        public string  Type { get; set; }
        public int? ApiResourceId { get; set; }
    }
    public class ApiResourcePropertiesContract
    {
        public int? Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int? ApiResourceId { get; set; }
    }
    public class ApiResourceScopesContract
    {
        public int? Id { get; set; }
        public string Scope { get; set; }
        public int? ApiResourceId { get; set; }
    }
    public class ApiResourceSecretsContract
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime Expiration { get; set; }
        public string Type { get; set; }
        public DateTime Created { get; set; }
        public int? ApiResourceId { get; set; }
    }
}
