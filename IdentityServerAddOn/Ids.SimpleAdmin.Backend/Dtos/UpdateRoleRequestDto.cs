using System.Collections.Generic;

namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class UpdateRoleRequestDto
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
