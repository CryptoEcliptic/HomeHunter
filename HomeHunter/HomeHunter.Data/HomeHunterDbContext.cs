using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Data
{
    public class HomeHunterDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public HomeHunterDbContext(DbContextOptions<HomeHunterDbContext> options)
            : base(options)
        {

        }
    }
}
