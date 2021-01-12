using System;
using System.Collections.Generic;

namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class CreateApiResourceRequestDto
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string AllowedAccessTokenSigningAlgorithmes { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public DateTime Created { get; set; }
        public bool NonEditable { get; set; }
        public Dictionary<string,string> Claims { get; set; }
        public Dictionary<string, ApiResourcePropertyResponseDto> Properties { get; set; }
        public Dictionary<string, string> Scopes { get; set; }
        public Dictionary<string, ApiResourceSecretsResponseDto> Secrets { get; set; }
    }
}