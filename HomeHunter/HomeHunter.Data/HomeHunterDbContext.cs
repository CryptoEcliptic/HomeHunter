using HomeHunter.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeHunter.Data
{
    public class HomeHunterDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<HomeHunterUser> HomeHunterUsers { get; set; }

        public HomeHunterDbContext(DbContextOptions<HomeHunterDbContext> options)
            : base(options)
        {

        }
    }
}
