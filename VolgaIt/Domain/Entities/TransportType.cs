namespace VolgaIt.Domain.Entities
{
    public class TransportType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public static string AllTypeNormalizedName => "ALL";
    }
}
