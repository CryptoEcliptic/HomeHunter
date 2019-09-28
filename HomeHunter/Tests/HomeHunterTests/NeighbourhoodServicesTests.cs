using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services;
using HomeHunterTests.Common;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunterTests
{
    [TestFixture]
    public class NeighbourhoodServicesTests
    {
        private const string NeighbourhoodNameMismatchMessage = "Returned neighbourhood name does not match with the expected one!";

        private const string NeighbourhoodCountMismatchMessage = "Expected neighbourhoods count defers from actual one!";

        private List<Neighbourhood> TestDataNeibourhoods = new List<Neighbourhood>
        {
            new Neighbourhood { Name = "Дружба", City = new City{ Name= "София", Id= 500} },
            new Neighbourhood { Name = "Младост", CityId = 500,},
            new Neighbourhood { Name = "Люлин", CityId = 500},
        };
        private HomeHunterDbContext context;

        public NeighbourhoodServicesTests()
        {
            this.context = InMemoryDatabase.GetDbContext();
            this.SeedData();
        }

        [Theory]
        [TestCase("Младост", "Младост")]
        [TestCase("Бруклин", null)]
        [TestCase(null, null)]
        public async Task GetNeighbourhoodByNameShouldReturnNeighbourhoodIfValidNameProvidedOrNullIfInvalidName(string name, string expectedResult)
        {
            var service = new NeighbourhoodServices(context);
            var neighbourhood = await service.GetNeighbourhoodByNameAsync(name);
            var actualResult = neighbourhood == null ? null : neighbourhood.Name;

            Assert.That(actualResult == expectedResult, NeighbourhoodNameMismatchMessage);
        }

        [Theory]
        [TestCase("София", 3)]
        [TestCase("Париж", 0)]
        [TestCase(null, 0)]
        public async Task GetNeighbourhoodByCityNameShouldReturnNeighbourhoodOrNull(string name, int expectedResult)
        {
            var service = new NeighbourhoodServices(context);
            var neighbourhoods = await service.GetNeighbourhoodsByCityAsync(name);
            var actualResult = neighbourhoods == null ? 0 : neighbourhoods.AsQueryable().Count();

            Assert.That(actualResult == expectedResult, NeighbourhoodCountMismatchMessage);
        }

        private void SeedData()
        {
            context.Neighbourhoods.AddRange(TestDataNeibourhoods);
            context.SaveChanges();
        }
    }
}
