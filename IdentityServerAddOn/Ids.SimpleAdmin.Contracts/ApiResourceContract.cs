using System;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class ApiResourceContract : Identifiable<int?>
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
        public List<ClaimsContract> UserClaims { get; set; }
        public List<PropertyContract> Properties { get; set; }
        public List<ScopeContract> Scopes { get; set; }
        public List<ApiResourceSecretsContract> Secrets { get; set; }
    }
}
