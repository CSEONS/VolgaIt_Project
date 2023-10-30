using VolgaIt.Domain.Entities;

namespace VolgaIt.MediatR.RentCommands.Models
{
    public class RentViewModel
    {
        public string Id { get; set; }
        public string RentType { get; set; }
        public string? UserId { get; set; }
        public string? TransportId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double PriceOfUnit { get; set; }
        public double FinalPrice { get; set; }
    }
}
