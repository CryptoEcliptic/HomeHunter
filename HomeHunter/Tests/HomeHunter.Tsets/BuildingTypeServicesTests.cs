using NUnit.Framework;
using System.Collections.Generic;

namespace HomeHunter.Tsets
{
    [TestFixture]
    public class BuildingTypeServicesTests
    {
        [SetUp]
        public void Setup()
        {

        }

        //[Test]
        //public void GetAllBuildingTypesCountShouldReturnTwo()
        //{
        //    var options = new DbContextOptionsBuilder<HomeHunterDbContext>()
        //           .UseInMemoryDatabase(databaseName: "BuildingType_Database")
        //           .Options;

        //    var context = new HomeHunterDbContext(options);
        //    var expectedResultCount = 2;
        //    var buildingTypeInput = new List<BuildingType>()
        //    {
        //        new BuildingType { Name = "ЕПК"},
        //        new BuildingType { Name = "Панелка баце"}
        //    };

        //    context.BuildingTypes.AddRange(buildingTypeInput);
        //    context.SaveChanges();

        //    var buildingTypesService = new BuildingTypeServices(context);
        //    var actualResult = buildingTypesService.GetAllBuildingTypes();

        //    Assert.That(actualResult.Count, Is.EqualTo(expectedResultCount));
        //}
    }
}
