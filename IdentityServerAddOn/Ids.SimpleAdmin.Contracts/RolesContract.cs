﻿using System.Collections.Generic;

namespace Ids.SimpleAdmin.Contracts
{
    public class RolesContract : Identifiable<string>
    {
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public List<ValueClaimsContract> Claims { get; set; }
    }
}
