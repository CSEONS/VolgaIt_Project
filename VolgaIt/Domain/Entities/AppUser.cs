using Microsoft.AspNetCore.Identity;

namespace VolgaIt.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? AccesToken { get; set; }
        public string? RefreshToken { get; set; }
        public virtual ICollection<Transport> Transports { get; set; }
        public double Balance { get; set; }
    }
}
