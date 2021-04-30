using System;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class UserContract : Identifiable<string>
    {
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string SetPassword { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string  PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public List<string> UserRoles { get; set; }
        public List<UserClaimsContract> UserClaims { get; set; }= new();
    }
    public class UserClaimsContract : Identifiable<int?>
    {
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
