namespace Ids.SimpleAdmin.Contracts
{
    public class PropertyContract : Identifiable<int?>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
