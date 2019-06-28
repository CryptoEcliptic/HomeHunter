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



        public HomeHunterDbContext(DbContextOptions<HomeHunterDbContext> options)
            : base(options)
        {

        }
    }
}
