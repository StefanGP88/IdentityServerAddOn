namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class UserClaimResponseDto
    {
        public ClaimUserResponseDto User { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
    public class ClaimUserResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
