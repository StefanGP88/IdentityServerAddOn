using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class RolesContract : Identifyable<string>
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Concurrencystamp { get; set; }
        public List<RoleClaimsContract> RoleClaims { get; set; }
    }
    public class RoleClaimsContract : Identifyable<int?>
    {
        public string RoleId { get; set; }
        public string ClainType { get; set; }
        public string ClaimValue { get; set; }
    }
}
