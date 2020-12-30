using System.Collections.Generic;
using System.Security.Claims;

namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class RoleResponseDto
    {
        public string  RoleName { get; set; }
        public string NormalizedName{ get; set; }
        public string Id { get; set; }
        public string ConcurrencyStamp { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
