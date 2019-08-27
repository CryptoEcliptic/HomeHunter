using HomeHunter.Domain;
using HomeHunter.Services;
using HomeHunterTests.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunterTests
{
    public class BuildingTypeServicesTests
    {
        private const string ResultCountMismatchMessage = "Expected test result should be 3";
        private const string ExpectedTrueResultMessage = "Expected result should return true, but it is false!";

        private List<BuildingType> TestData = new List<BuildingType>
        {
            new BuildingType { Name = "ЕПК", Id = 1},
            new BuildingType { Name = "Панел", Id = 2},
            new BuildingType { Name = "Тухла", Id = 3},
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
