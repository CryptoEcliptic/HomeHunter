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
        private const string ResultCountMismatchMessage = "Expected test result should be 3";
        private const string ExpectedTrueResultMessage = "Expected result should return true, but it is false!";

        private List<BuildingType> TestData = new List<BuildingType>
        {
            new BuildingType { Name = "ЕПК", },
            new BuildingType { Name = "Панел", Id = RandomIdGenerator.GenerateRandomIntId()},
            new BuildingType { Name = "Тухла", Id = RandomIdGenerator.GenerateRandomIntId()},
        };

        public BuildingTypeServicesTests()
        {
            this.SeedData();
        }

        [Test]
        public async Task GetAllBuildingTypesCountShouldReturnThree()
        {
            var context = InMemoryDatabase.GetDbContext();

            var buildingTypesService = new BuildingTypeServices(context);
            var buildingTypes = await buildingTypesService.GetAllBuildingTypesAsync();

            int numberOftypes = buildingTypes.AsQueryable().Count();
            var expectedResultCount = 3;

            Assert.That(numberOftypes, Is.EqualTo(expectedResultCount), ResultCountMismatchMessage);
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
           
            Assert.That(actualResult, Is.EqualTo(expectedResult), ExpectedTrueResultMessage);
        }

        private void SeedData()
        {
            var context = InMemoryDatabase.GetDbContext();
            context.BuildingTypes.AddRange(TestData);
            context.SaveChanges();
        }
    }
}
