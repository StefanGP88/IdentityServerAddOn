using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ids.SimpleAdmin.Contracts
{
    public class IdentityResourceContract : Identifiable<int?>
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }//TODO: make sure nullable does not mess with validators
        public bool NonEditable { get; set; }
        public List<IdentityResourceClaimsContract> UserClaims { get; set; }
        public List<IdentityResourcePropertiesContract> Properties { get; set; }
    }
    public class IdentityResourcePropertiesContract : Identifiable<int?>
    {
        public int? IdentityResourceId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class IdentityResourceClaimsContract : Identifiable<int?>
    {
        public int? IdentityResourceId { get; set; }
        public string Type { get; set; }
    }
}
