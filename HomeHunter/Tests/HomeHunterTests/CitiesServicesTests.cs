using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services;
using HomeHunterTests.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunterTests
{
    [TestFixture]
    public class CitiesServicesTests
    {
        private const string ResultCountMismatchMessage = "Expected test result should be 3";
        private const string CityNameMismatchMessage = "Returned city name does not match with the expected one!";

        private List<City> TestData = new List<City>
        { 
            new City { Name = "София", Id = 1, },
            new City { Name = "Враца", Id = 2,},
            new City { Name = "Ботевград", Id = 3, },
        };
        private HomeHunterDbContext context;

        public CitiesServicesTests()
        {
            this.context = InMemoryDatabase.GetDbContext();
            this.SeedData(); 
        }

        [Test]
        public async Task GetAllCitiesCountShouldReturnThree()
        {

            var service = new CitiesServices(context);
            var cities = await service.GetAllCitiesAsync();

            int numberOftypes = cities.AsQueryable().Count();
            var expectedResultCount = 3;

            Assert.That(numberOftypes, Is.EqualTo(expectedResultCount), ResultCountMismatchMessage);
            Assert.That(TestData.FirstOrDefault().Name == cities.FirstOrDefault().Name);
        }

        [Test]
        [TestCase("Враца", "Враца")]
        [TestCase("Бацова маала", null)]
        [TestCase(null, null)]
        public async Task GetCityByNameShouldReturnCityIfValidNameProvidedOrNullIfInvalidName(string cityName, string expectedResult)
        {
            var service = new CitiesServices(context);
            var city = await service.GetByNameAsync(cityName);
            var actualResult = city == null ? null : city.Name;

            Assert.That(actualResult == expectedResult, CityNameMismatchMessage);
        }

        private void SeedData()
        {
            context.Cities.AddRange(TestData);
            context.SaveChanges();
        }

    }
}
