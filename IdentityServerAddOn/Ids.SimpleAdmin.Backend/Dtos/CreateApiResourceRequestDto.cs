using System;

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
    }
}
