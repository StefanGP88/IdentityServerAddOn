using System.Collections.Generic;

namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class CreateRoleRequestDto
    {
        public string RoleName { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
