using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VolgaIt.Domain.Entities;

namespace VolgaIt.Domain
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<TransportType> TransportTypes { get; set; }
        public DbSet<Rent> Rents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var adminRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            var adminUser = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "superpassword123")
            };

            var adminUserRole = new IdentityUserRole<string>()
            {
                RoleId = adminRole.Id,
                UserId = adminUser.Id,
            };

            builder.Entity<AppUser>().HasData(adminUser);
            builder.Entity<IdentityRole>().HasData(adminRole);
            builder.Entity<IdentityUserRole<string>>().HasData(adminUserRole);

            List<TransportType> transportTypes = new List<TransportType>()
            {
                new TransportType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Car",
                    NormalizedName = "CAR"
                },
                new TransportType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Bike",
                    NormalizedName = "BIKE"
                },
                new TransportType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Scooter",
                    NormalizedName = "SCOOTER"
                }
            };

            builder.Entity<TransportType>().HasData(transportTypes);

            base.OnModelCreating(builder);
        }
    }
}
