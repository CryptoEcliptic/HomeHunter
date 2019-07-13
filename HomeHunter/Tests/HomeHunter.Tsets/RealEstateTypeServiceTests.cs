
using NUnit.Framework;
using System.Collections.Generic;

namespace HomeHunter.Tsets
{
    [TestFixture]
    public class RealEstateTypeServiceTests
    {
       
        [SetUp]
        public void Setup()
        {

        }

        //[Test]
        //public void GetAllRealEstateTypesCountShouldReturnTwo()
        //{
        //    var options = new DbContextOptionsBuilder<HomeHunterDbContext>()
        //           .UseInMemoryDatabase(databaseName: "RealEstateTypes_Database")
        //           .Options;

        //    var context = new HomeHunterDbContext(options);
        //    var expectedResultCount = 2;
        //    var realEstateInput = new List<RealEstateType>()
        //    {
        //        new RealEstateType { TypeName = "Мезонет"},
        //        new RealEstateType { TypeName = "Двустаен апартамент"}
        //    };

        //    context.RealEstateTypes.AddRange(realEstateInput);
        //    context.SaveChanges();

        //    var realEstateTypesService = new RealEstateTypeServices(context);
        //    var actualResult = realEstateTypesService.GetAllTypes();

        //    Assert.That(actualResult.Count, Is.EqualTo(expectedResultCount));
        //}
    }
}
