using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class UserResponseDto
    {
        public string Userid { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public bool Use2Fa { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime LockoutEnd { get; set; }
        public string ConcurrencyStamp { get; set; }
        public List<string> Roles { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
