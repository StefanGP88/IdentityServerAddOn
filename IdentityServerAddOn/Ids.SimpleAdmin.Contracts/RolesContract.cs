using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class RolesContract : Identifyable<string>
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public List<RoleClaimsContract> RoleClaims { get; set; }
    }
    public class RoleClaimsContract : Identifyable<int?>
    {
        public string RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
