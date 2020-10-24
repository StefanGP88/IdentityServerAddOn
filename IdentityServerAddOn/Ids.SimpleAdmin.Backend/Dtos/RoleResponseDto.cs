namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class RoleResponseDto
    {
        public string  RoleName { get; set; }
        public string NormalizedName{ get; set; }
        public string Id { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
