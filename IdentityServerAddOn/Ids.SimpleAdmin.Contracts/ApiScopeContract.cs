using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class ApiScopeContract : Identifyable<int?>
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public List<ApiScopeClaimsContract> UserClaims { get; set; }
        public List<ApiScopePropertiesContract> Properties { get; set; }
    }
    public class ApiScopeClaimsContract : Identifyable<int?>
    {
        public string Type { get; set; }
        public int? ApiScopeId { get; set; }
    }
    public class ApiScopePropertiesContract : Identifyable<int?>
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int? ApiScopeId { get; set; }
    }
}
