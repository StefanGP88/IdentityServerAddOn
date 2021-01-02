using System.Collections.Generic;

namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class ApiScopeResponseDto
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public Dictionary<int , string > Claims { get; set; }
        public Dictionary<string , ApiScopePropertyResponseDto> Properties { get; set; }
    }
    public class ApiScopePropertyResponseDto
    {
        public string Key { get; set; }
        public string  PropertyValue { get; set; }
    }
}
