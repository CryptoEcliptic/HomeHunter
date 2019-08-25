using HomeHunter.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Tsets.Common
{
    public class InMemoryDatabase
    {
        public static HomeHunterDbContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HomeHunterDbContext>()
                .UseInMemoryDatabase("TestDb");

            var context = new HomeHunterDbContext(optionsBuilder.Options);

            return context;
        }
    }
}
