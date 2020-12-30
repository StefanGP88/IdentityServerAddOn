using System;
using System.Collections.Generic;
using System.Text;

namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class UpdateApiScopeRequestDto
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public Dictionary<int, string> Claims { get; set; }
        //public Dictionary<string, string> Properties { get; set; }
    }
}
