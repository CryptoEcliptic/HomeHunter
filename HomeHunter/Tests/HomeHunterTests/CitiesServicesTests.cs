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
    [TestFixture]
    public class CitiesServicesTests
    {
        private const string ResultCountMismatchMessage = "Expected test result should be 3";
        private const string CityNameMismatchMessage = "Returned city name does not match with the expected one!";
        private const string ResultShouldBNullMessage = "Actual result should be null, but it is not!";
        private List<City> TestData = new List<City>
        { 
            new City { Name = "София", Id = 1, },
            new City { Name = "Враца", Id = 2,},
            new City { Name = "Ботевград", Id = 3, },
        };

        public CitiesServicesTests()
        {
            this.SeedData();
        }

        [Test]
        public async Task GetAllCitiesCountShouldReturnThree()
        {
            var context = InMemoryDatabase.GetDbContext();

            var service = new CitiesServices(context);
            var cities = await service.GetAllCitiesAsync();

            int numberOftypes = cities.AsQueryable().Count();
            var expectedResultCount = 3;

            Assert.That(numberOftypes, Is.EqualTo(expectedResultCount), ResultCountMismatchMessage);
            Assert.That(TestData.FirstOrDefault().Name == cities.FirstOrDefault().Name);
        }

        [Test]
        [TestCase("Враца")]
        [TestCase("Бацова маала")]
        [TestCase(null)]
        public async Task GetCityByNameShouldReturnCityIfValidNameProvidedOrNullIfInvalidName(string cityName)
        {
            var context = InMemoryDatabase.GetDbContext();
            var service = new CitiesServices(context);
            var city = await service.GetByNameAsync(cityName);
            var actualResult = city == null ? null : city.Name;

            if (actualResult == null && cityName != null)
            {
                Assert.That(actualResult == null, ResultShouldBNullMessage);
            }
            else
            {
                Assert.That(actualResult == cityName, CityNameMismatchMessage);
            }   
            
        }

        private void SeedData()
        {
            var context = InMemoryDatabase.GetDbContext();
            context.Cities.AddRange(TestData);
            context.SaveChanges();
        }
    }
}
