
using NUnit.Framework;
using System.Collections.Generic;

namespace HomeHunter.Tsets
{
    [TestFixture]
    public class HeatingSystemServiceTests
    {
        [SetUp]
        public void Setup()
        {

        }

        //[Test]
        //public void GetAllHeatingSystemCountShouldReturnTwo()
        //{
        //    var options = new DbContextOptionsBuilder<HomeHunterDbContext>()
        //           .UseInMemoryDatabase(databaseName: "HeatingSystems_Database")
        //           .Options;

        //    var context = new HomeHunterDbContext(options);
        //    var expectedResultCount = 2;
        //    var heatingSystemsInput = new List<HeatingSystem>()
        //    {
        //        new HeatingSystem {  Name = "ТЕЦ"},
        //        new HeatingSystem { Name = "Бойлер на дърва"}
        //    };

        //    context.HeatingSystems.AddRange(heatingSystemsInput);
        //    context.SaveChanges();

        //    var heatingSystemsTypesService = new HeatingSystemServices(context);
        //    var actualResult = heatingSystemsTypesService.GetAllHeatingSystems();

        //    Assert.That(actualResult.Count, Is.EqualTo(expectedResultCount));
        //}
    }
}
