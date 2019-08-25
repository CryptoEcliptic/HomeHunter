using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeHunter.Services.Models.BuildingType;
using AutoMapper;
using HomeHunter.Models.ViewModels.BuildingType;
using HomeHunter.Infrastructure;
using HomeHunter.Tsets.Common;
using System;

namespace HomeHunter.Tsets
{
    public class BuildingTypeServicesTests
    {
       

        private List<BuildingType> TestData = new List<BuildingType>
        {
            new BuildingType { Name = "ЕПК", Id = RandomIdGenerator()},
            new BuildingType { Name = "Панел", Id = RandomIdGenerator()},
            new BuildingType { Name = "Тухла", Id = RandomIdGenerator()},
        };

        public BuildingTypeServicesTests()
        {
            this.SeedData();
        }

        [Test]
        public async Task GetAllBuildingTypesCountShouldReturnTwo()
        {
            var context = InMemoryDatabase.GetDbContext();

            var buildingTypesService = new BuildingTypeServices(context);
            var buildingTypes = await buildingTypesService.GetAllBuildingTypesAsync();

            int numberOftypes = buildingTypes.AsQueryable().Count();
            var expectedResultCount = 3;

            Assert.That(numberOftypes, Is.EqualTo(expectedResultCount));
        }

        [Test]
        [TestCase("ЕПК")]
        [TestCase(null)]
        public async Task GetBuildingTypeByNameShouldReturnTrue(string type)
        {
            var context = InMemoryDatabase.GetDbContext();
            var expectedResult = type != null ? "ЕПК" : null;

            var buildingTypesService = new BuildingTypeServices(context);
            var buildingType = await buildingTypesService.GetBuildingTypeAsync(type);

            string actualResult = buildingType != null ? buildingType.Name : null;
           
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        private void SeedData()
        {
            var context = InMemoryDatabase.GetDbContext();
            context.BuildingTypes.AddRange(TestData);
            context.SaveChanges();
        }

        private static int RandomIdGenerator()
        {
            Random rnd = new Random();
            int id = rnd.Next(1, 100000);

            return id;
        }
    }
}
