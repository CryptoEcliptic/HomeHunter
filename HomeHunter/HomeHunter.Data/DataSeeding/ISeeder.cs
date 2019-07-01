using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunter.Data.DataSeeding
{
    public interface ISeeder
    {
       Task SeedAsync(HomeHunterDbContext dbContext, IServiceProvider serviceProvider);
    }
}
