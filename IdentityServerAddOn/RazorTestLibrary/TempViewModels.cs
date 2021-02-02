using System;

namespace RazorTestLibrary
{
    public class PropertRowModel
    {
        public string Property { get; set; }
        public string PropertyKey { get; set; }
    }

    public class SecretRowModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
    }
}
