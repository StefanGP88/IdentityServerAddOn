using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class ApiScopeContract : Identifiable<int?>
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public List<ClaimsContract> UserClaims { get; set; }
        public List<PropertyContract> Properties { get; set; }
    }



}
