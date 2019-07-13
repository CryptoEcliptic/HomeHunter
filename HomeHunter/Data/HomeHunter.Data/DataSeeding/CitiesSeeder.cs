using HomeHunter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Data.DataSeeding
{
    public class CitiesSeeder : ISeeder
    {
        private readonly string[] Cities = new string[]
        {
            "София",
            "Перник",
            "Самоков",
            "Банкя",
            "Кюстендил",
            "Ботевград",
            "Радомир",
            "Ихтиман",
            "Берковица"
        };

        public async Task SeedAsync(HomeHunterDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedCitiesAsync(Cities, dbContext);
        }

        private static async Task SeedCitiesAsync(string[] cities, HomeHunterDbContext dbContext)
        {
            var citiesFromDb = dbContext.Cities.ToList();

            var createdCities = new List<City>();

            foreach (var name in cities)
            {
                if (!citiesFromDb.Any(x => x.Name == name))
                {
                    createdCities.Add(new City
                    {
                        Name = name,
                        CreatedOn = DateTime.UtcNow,
                    });
                }
            }

            await dbContext.Cities.AddRangeAsync(createdCities);
            await dbContext.SaveChangesAsync();
        }
    }
}
