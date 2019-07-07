using HomeHunter.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeHunter.Data
{
    public class HomeHunterDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<HomeHunterUser> HomeHunterUsers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Village> Villages { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Neighbourhood> Neighbourhoods { get; set; }
        public DbSet<HeatingSystem> HeatingSystems { get; set; }
        public DbSet<RealEstateType> RealEstateTypes { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }


        public HomeHunterDbContext(DbContextOptions<HomeHunterDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RealEstate>()
                .HasOne(x => x.Address)
                .WithOne(x => x.RealEstate)
                .HasForeignKey<Address>(x => x.RealEstateId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}
