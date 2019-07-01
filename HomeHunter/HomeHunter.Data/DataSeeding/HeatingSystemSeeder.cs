using HomeHunter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Data.DataSeeding
{
    public class HeatingSystemSeeder : ISeeder
    {
        private readonly string[] HeatingSystemsList = new string[] {
            "ТЕЦ",
            "Ток",
            "Природен газ",
            "Твърдо гориво",
            "Локално отопление"
        };

        public async Task SeedAsync(HomeHunterDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedHeatingSystemsAsync(HeatingSystemsList, dbContext);
        }

        private static async Task SeedHeatingSystemsAsync(string[] heatingSystemsList, HomeHunterDbContext dbContext)
        {
            var heatingSystemTypesFromDb = dbContext.HeatingSystems.ToList();
            var createdTypes = new List<HeatingSystem>();

            foreach (var type in heatingSystemsList)
            {
                if (!heatingSystemTypesFromDb.Any(x => x.Name == type))
                {
                    createdTypes.Add(new HeatingSystem{ Name = type, CreatedOn = DateTime.UtcNow });
                }
            }

            await dbContext.HeatingSystems.AddRangeAsync(createdTypes);
            await dbContext.SaveChangesAsync();
        }
    }
}
