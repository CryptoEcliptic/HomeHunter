using HomeHunter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Data.DataSeeding
{
    public class RealEstateTypesSeeder : ISeeder
    {
        private readonly string[] RealEstateTypesList = new string[] {

            "Едностаен апартамент",
            "Двустаен апартамент",
            "Тристаен апартамент",
            "Четиристаен апартамент",
            "Многостаен апартамент",
            "Мезонет",
            "Ателие, Таван",
            "Офис",
            "Магазин",
            "Заведение",
            "Склад",
            "Промишлено помещение",
            "Етаж от къща",
            "Къща",
            "Гараж",
            "Парцел",
            "Земеделска земя",
            "Хотел"
        };
        
        public async Task SeedAsync(HomeHunterDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedRealEstateTypesAsync(RealEstateTypesList, dbContext);
        }

        private static async Task SeedRealEstateTypesAsync(string[] realEstateTypes, HomeHunterDbContext dbContext)
        {
            var realEstateTypesFromDb = dbContext.RealEstateTypes.ToList();
            var createdTypes = new List<RealEstateType>();

            foreach (var type in realEstateTypes)
            {
                if (!realEstateTypesFromDb.Any(x => x.TypeName == type))
                {
                    createdTypes.Add(new RealEstateType { TypeName = type, CreatedOn = DateTime.UtcNow });
                }
            }

            await dbContext.RealEstateTypes.AddRangeAsync(createdTypes);
            await dbContext.SaveChangesAsync();
        }
    }
}
