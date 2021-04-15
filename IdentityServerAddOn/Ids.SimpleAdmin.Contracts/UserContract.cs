using System;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class UserContract : Identifyable<string>
    {
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string ReplacePassword { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string  PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool ResetAccessFailedCount { get; set; }
        public List<Guid> UserRoles { get; set;}
        public List<UserClaimsContract> UserClaims { get; set; }
    }
    public class UserClaimsContract : Identifyable<string>
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
