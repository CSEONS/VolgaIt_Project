using System.Text.Json.Serialization;

namespace VolgaIt.Domain.Entities
{
    public class Transport
    {
        public string Id { get; set; }
        public bool CanBeRented { get; set; }
        public bool IsRented { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Identifier { get; set; }
        public string? TypeId { get; set; }
        public virtual TransportType? Type { get; set; }
        public string? Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? MinutePrice { get; set; }
        public double? DayPrice { get; set;}
        public string? OwnerId { get; set; }
        public virtual AppUser Owner { get; set; }
    }
}
