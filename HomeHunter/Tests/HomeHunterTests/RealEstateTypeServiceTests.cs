using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Models.ViewModels.RealEstateType;
using HomeHunter.Services;
using HomeHunter.Services.Models.RealEstateType;
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
    public class RealEstateTypeServiceTests
    {
        private const string ResultCountMismatchMessage = "Expected test result should be 3";
        private const string ExpectedTrueResultMessage = "Expected result should return true, but it is false!";

        private List<RealEstateType> TestData = new List<RealEstateType>
        {
            new RealEstateType { TypeName = "Едностаен апартамент", Id = 1},
            new RealEstateType { TypeName = "Двустаен апартамент", Id = 2},
            new RealEstateType { TypeName = "Тристаен апартамент", Id = 3},
        };

        private HomeHunterDbContext context;

        public RealEstateTypeServiceTests()
        {
            this.context = InMemoryDatabase.GetDbContext();
            this.SeedData();
        }

        [Test]
        public async Task GetAllRealEstateTypesCountShouldReturnThree()
        {
            var mapper = this.GetMapper();

            var realEstateTypeService = new RealEstateTypeServices(context);
            var realEstateTypeServiceModel = await realEstateTypeService.GetAllTypesAsync();
            var realEstateTypesViewModel = mapper.Map<List<RealEstateTypeViewModel>>(realEstateTypeServiceModel);

            int numberOftypes = realEstateTypesViewModel.Count();
            var expectedResultCount = 3;

            Assert.That(numberOftypes, Is.EqualTo(expectedResultCount), ResultCountMismatchMessage);
        }

        [Test]
        [TestCase("Едностаен апартамент", "Едностаен апартамент")]
        [TestCase(null, null)]
        public async Task GetRealEstateTypeByNameShouldReturnTrue(string type, string expectedResult)
        {
            var realEstateTypeService = new RealEstateTypeServices(context);
            var realEstateType = await realEstateTypeService.GetRealEstateTypeByNameAsync(type);

            string actualResult = realEstateType != null ? realEstateType.TypeName : null;

            Assert.That(actualResult, Is.EqualTo(expectedResult), ExpectedTrueResultMessage);
        }

        private void SeedData()
        {
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
