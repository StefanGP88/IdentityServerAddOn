using System;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class ApiResourceContract: Identifiable<int?>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string AllowedAccessTokenSigningAlgorithms { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }//TODO: make sure nulalble is not causing trouble in validators
        public DateTime? LastAccessed { get; set; }//TODO: make sure nulalble is not causing trouble in validators
        public bool Enabled { get; set; }
        public bool NonEditable { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public List<ApiResourceClaimsContract> UserClaims { get; set; }
        public List<ApiResourcePropertiesContract> Properties { get; set; }
        public List<ApiResourceScopesContract> Scopes { get; set; }
        public List<ApiResourceSecretsContract> Secrets { get; set; }
    }
    public class ApiResourceClaimsContract : Identifiable<int?>
    {
        public string  Type { get; set; }
        public int? ApiResourceId { get; set; }
    }
    public class ApiResourcePropertiesContract : Identifiable<int?>
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int? ApiResourceId { get; set; }
    }
    public class ApiResourceScopesContract : Identifiable<int?>
    {
        public string Scope { get; set; }
        public int? ApiResourceId { get; set; }
    }
    public class ApiResourceSecretsContract : Identifiable<int?>
    {
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }//TODO: make sure nulalble is not causing trouble in validators
        public string Type { get; set; }
        public DateTime Created { get; set; }
        public int? ApiResourceId { get; set; }
    }
}
