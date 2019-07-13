using HomeHunter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunter.Data.DataSeeding
{
    public class BuildingTypeSeeder : ISeeder
    {
        private readonly string[] BuildingTypes = new string[]
       {
            "Панел",
            "ЕПК",
            "Тухла",
            "Ново строителство",
            "Пълзящ куфраж",
       };

        public async Task SeedAsync(HomeHunterDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedCitiesAsync(BuildingTypes, dbContext);
        }

        private static async Task SeedCitiesAsync(string[] buildingTypes, HomeHunterDbContext dbContext)
        {
            var buildingTypesFromDb = dbContext.BuildingTypes.ToList();

            var createdBuildigTypes = new List<BuildingType>();

            foreach (var type in buildingTypes)
            {
                if (!buildingTypesFromDb.Any(x => x.Name == type))
                {
                    createdBuildigTypes.Add(new BuildingType
                    {
                        Name = type,
                        CreatedOn = DateTime.UtcNow,
                    });
                }
            }

            await dbContext.BuildingTypes.AddRangeAsync(createdBuildigTypes);
            await dbContext.SaveChangesAsync();
        }
    }
}
