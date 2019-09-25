using HomeHunter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Data.DataSeeding
{
    public class RealEstateTypesSeeder : ISeeder
    {
        private readonly RealEstateType[] RealEstateTypesList = new RealEstateType[] {

            new RealEstateType{ TypeName = "Едностаен апартамент", MinReferenceNumber = "0001", MaxReferenceNumber = "0199"},
            new RealEstateType{ TypeName = "Двустаен апартамент", MinReferenceNumber = "0200", MaxReferenceNumber = "0299"},
            new RealEstateType{ TypeName = "Тристаен апартамент", MinReferenceNumber = "0300", MaxReferenceNumber = "0399"},
            new RealEstateType{ TypeName = "Четиристаен апартамент", MinReferenceNumber = "0400", MaxReferenceNumber = "0499"},
            new RealEstateType{ TypeName = "Многостаен апартамент", MinReferenceNumber = "0500", MaxReferenceNumber = "0599"},
            new RealEstateType{ TypeName = "Мезонет", MinReferenceNumber = "0600", MaxReferenceNumber = "0699"},
            new RealEstateType{ TypeName = "Ателие, Таван", MinReferenceNumber = "0700", MaxReferenceNumber = "0799"},
            new RealEstateType{ TypeName = "Офис", MinReferenceNumber = "0800", MaxReferenceNumber = "0899"},
            new RealEstateType{ TypeName = "Магазин", MinReferenceNumber = "0900", MaxReferenceNumber = "0999"},
            new RealEstateType{ TypeName = "Заведение", MinReferenceNumber = "1000", MaxReferenceNumber = "1099"},
            new RealEstateType{ TypeName = "Склад", MinReferenceNumber = "1100", MaxReferenceNumber = "1199"},
            new RealEstateType{ TypeName = "Промишлено помещение", MinReferenceNumber = "1200", MaxReferenceNumber = "1299"},
            new RealEstateType{ TypeName = "Етаж от къща", MinReferenceNumber = "1300", MaxReferenceNumber = "1399"},
            new RealEstateType{ TypeName = "Къща", MinReferenceNumber = "1400", MaxReferenceNumber = "1499"},
            new RealEstateType{ TypeName = "Гараж", MinReferenceNumber = "1500", MaxReferenceNumber = "1599"},
            new RealEstateType{ TypeName = "Парцел", MinReferenceNumber = "1600", MaxReferenceNumber = "1699"},
            new RealEstateType{ TypeName = "Земеделска земя", MinReferenceNumber = "1700", MaxReferenceNumber = "1799"},
            new RealEstateType{ TypeName = "Хотел", MinReferenceNumber = "1800", MaxReferenceNumber = "1899"},
            
        };
        
        public async Task SeedAsync(HomeHunterDbContext dbContext, IServiceProvider serviceProvider)
        {
            await SeedRealEstateTypesAsync(RealEstateTypesList, dbContext);
        }

        private static async Task SeedRealEstateTypesAsync(RealEstateType[] realEstateTypes, HomeHunterDbContext dbContext)
        {
            var realEstateTypesFromDb = dbContext.RealEstateTypes.ToList();

            foreach (var type in realEstateTypes)
            {
                if (!realEstateTypesFromDb.Any(x => x.TypeName == type.TypeName))
                {
                    await dbContext.RealEstateTypes.AddAsync(type);
                    
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
