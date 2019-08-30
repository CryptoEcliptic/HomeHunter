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
    public class VillageServicesTests
    {
        private const string ResultShuoldBeTrueMessage = "Expected result should be true, but it ts false";
        private const string ResultShouldBNullMessage = "Actual result should be null, but it is not!";

        private List<Village> TestData = new List<Village>
        {
            new Village { Name = "Волуяк", Id = 1, },
            new Village { Name = "Пасарел", Id = 2,},
            new Village { Name = "Ново Село", Id = 3, },
        };

        private HomeHunterDbContext context;

        public VillageServicesTests()
        {
            this.context = InMemoryDatabase.GetDbContext();
            this.SeedData();
        }

        [Test]
        public async Task CreateVillageShouldReturnTrue()
        {
            var expectedResult = new Village { Id = 1, Name = "Бацова маала" };
            var newVillageName = "Бацова маала";

            var context = this.GetDbContext();

            var villageServices = new VillageServices(context);
            var actualResult = await villageServices.CreateVillageAsync(newVillageName);

            Assert.That(expectedResult.Name.Equals(actualResult.Name), ResultShuoldBeTrueMessage);
            Assert.That(expectedResult.Id.Equals(actualResult.Id), ResultShuoldBeTrueMessage);

        }

        [Test]
        public async Task CreateVillageParameterNullShouldReturnNull()
        {
            var newVillageName = "";

            var villageServices = new VillageServices(context);
            var actualResult = await villageServices.CreateVillageAsync(newVillageName);

            Assert.That(actualResult == null, ResultShouldBNullMessage);

        }

        [Test]
        public async Task CreateVillageWithExistingNameShouldReturnTheVillage()
        {
            var existingVillageName = "Пасарел";
            var expectedVillageToBeReturned = TestData.FirstOrDefault(x => x.Name == existingVillageName);

            var villageServices = new VillageServices(context);
            var actualResult = await villageServices.CreateVillageAsync(existingVillageName);
            ;
            Assert.That(expectedVillageToBeReturned.Name.Equals(actualResult.Name), ResultShuoldBeTrueMessage);
            Assert.That(expectedVillageToBeReturned.Id.Equals(actualResult.Id), ResultShuoldBeTrueMessage);

        }

        private void SeedData()
        {
            context.Villages.AddRange(TestData);
            context.SaveChanges();
        }

        private HomeHunterDbContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HomeHunterDbContext>()
                .UseInMemoryDatabase("TestDb");

            var context = new HomeHunterDbContext(optionsBuilder.Options);

            return context;
        }
    }
}
