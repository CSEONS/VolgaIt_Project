using VolgaIt.Domain.Entities;
using VolgaIt.MediatR.TransportCommands.Models;

namespace VolgaIt.MediatR.AccountCommands.Models
{
    public class AppUserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public double Balance { get; set; }
        public List<TransportViewModel> Transports { get; set; }
    }
}
