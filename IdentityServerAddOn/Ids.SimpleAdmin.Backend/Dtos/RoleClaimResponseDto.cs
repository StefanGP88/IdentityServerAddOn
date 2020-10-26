﻿namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class RoleClaimResponseDto
    {
        public ClaimRoleResponseDto Role { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
    public class ClaimRoleResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
