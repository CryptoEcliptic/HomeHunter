
using AutoMapper;
using HomeHunter.Domain;
using HomeHunter.Models.ViewModels.RealEstateType;
using HomeHunter.Services;
using HomeHunter.Services.Models.RealEstateType;
using HomeHunter.Tsets.Common;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Tsets
{
    [TestFixture]
    public class RealEstateTypeServiceTests
    {
        private const string ResultCountMismatchMessage = "Expected test result should be 3";
        private const string ExpectedTrueResultMessage = "Expected result should return true, but it is false!";

        private List<RealEstateType> TestData = new List<RealEstateType>
        {
            new RealEstateType { TypeName = "Едностаен апартамент", Id = RandomIdGenerator.GenerateRandomIntId()},
            new RealEstateType { TypeName = "Двустаен апартамент", Id = RandomIdGenerator.GenerateRandomIntId()},
            new RealEstateType { TypeName = "Тристаен апартамент", Id = RandomIdGenerator.GenerateRandomIntId()},
        };

        public RealEstateTypeServiceTests()
        {
            this.SeedData();
        }

        [Test]
        public async Task GetAllRealEstateTypesCountShouldReturnThree()
        {
            var context = InMemoryDatabase.GetDbContext();
            var mapper = this.GetMapper();

            var realEstateTypeService = new RealEstateTypeServices(context);
            var realEstateTypeServiceModel = await realEstateTypeService.GetAllTypesAsync();
            var realEstateTypesViewModel = mapper.Map<List<RealEstateTypeViewModel>>(realEstateTypeServiceModel);

            int numberOftypes = realEstateTypesViewModel.Count();
            var expectedResultCount = 3;

            Assert.That(numberOftypes, Is.EqualTo(expectedResultCount), ResultCountMismatchMessage);
        }

        [Test]
        [TestCase("Едностаен апартамент")]
        [TestCase(null)]
        public async Task GetRealEstateTypeByNameShouldReturnTrue(string type)
        {
            var context = InMemoryDatabase.GetDbContext();
            var expectedResult = type != null ? "Едностаен апартамент" : null;

            var realEstateTypeService = new RealEstateTypeServices(context);
            var realEstateType = await realEstateTypeService.GetRealEstateTypeByNameAsync(type);

            string actualResult = realEstateType != null ? realEstateType.TypeName : null;

            Assert.That(actualResult, Is.EqualTo(expectedResult), ExpectedTrueResultMessage);
        }

        private void SeedData()
        {
            var context = InMemoryDatabase.GetDbContext();
            context.RealEstateTypes.AddRange(TestData);
            context.SaveChanges();
        }

        private IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<RealEstateTypeServiceModel, RealEstateTypeViewModel>();
            });

            var mapper = new Mapper(configuration);
            return mapper;
        }
    }
}
