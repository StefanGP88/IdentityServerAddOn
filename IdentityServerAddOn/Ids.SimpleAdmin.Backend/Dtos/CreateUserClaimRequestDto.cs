namespace Ids.SimpleAdmin.Backend.Dtos
{
    public class CreateUserClaimRequestDto
    {
        public string  UserId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
