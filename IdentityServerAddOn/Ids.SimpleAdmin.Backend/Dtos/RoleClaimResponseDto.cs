namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class RoleClaimResponseDto
    {
        public RoleClaimRoleDto Role { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
    public class RoleClaimRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
