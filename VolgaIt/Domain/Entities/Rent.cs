namespace VolgaIt.Domain.Entities
{
    public class Rent
    {
        public string Id { get; set; }
        public string RentType { get; set; }
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public string? TransportId { get; set; }
        public Transport? Transport { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double PriceOfUnit { get; set; }
        public double FinalPrice { get; set; }

        public readonly static Dictionary<RentType, string> RentTypes = new Dictionary<RentType, string>()
        {
            { Entities.RentType.Minutes, "MINUTES" },
            { Entities.RentType.Days, "DAYS" }
        };

        public double CalculateFinalPrice()
        {
            var span = (EndTime - StartTime);

            if (RentType.ToUpper() == RentTypes[Entities.RentType.Minutes])
                return span.Value.TotalMinutes * PriceOfUnit;
            else
                return span.Value.TotalDays * PriceOfUnit;
        }
    }

    public enum RentType
    {
        Minutes,
        Days
    }
}
