using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.RealEstate;
using HomeHunterTests.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunterTests
{
    [TestFixture]
    public class RealEstateServiceTests
    {
        private const string InvalidReturnIdMessage = "Actual return Id is different from the expected one!";
        private const string InvalidAddressExceptionMessage = "Argument null exception should be thrown because parameter Address is invalid";
        private const string ParameterValueMismatchMessage = "Actual return parameter is different from the expected one!";
        private const string NonExistingRealEstateMessage = "Argument null exception should be thrown because no real estate with such Id in the database.";
        private readonly HomeHunterDbContext context;

        private readonly Mock<IRealEstateTypeServices> realEstateTypeServices;
        private readonly Mock<ICitiesServices> citiesServices;
        private readonly Mock<INeighbourhoodServices> neighbourhoodServices;
        private readonly Mock<IAddressServices> addressServices;
        private readonly Mock<IVillageServices> villageServices;
        private readonly Mock<IBuildingTypeServices> buildingTypeServices;
        private readonly Mock<IHeatingSystemServices> heatingSystemServices;
        private readonly IMapper mapper;

        private List<RealEstate> TestData = new List<RealEstate>
        {
            new RealEstate {
            Id = "myRealEstateId1",
            RealEstateTypeId = 1,
            Address = new Address{ Description = "ул. Гугутка 2", Id = 5, },
            Price = 10000,
            Area = 500,
            PricePerSquareMeter = 200,
            FloorNumber = "2",
            BuildingTotalFloors = 5,
            },

            new RealEstate {
            Id = "myRealEstateId2",
            RealEstateTypeId = 2,
            Address = new Address{ Description = "ул. Пеперудка 2", Id = 6},
            Price = 50000,
            Area = 500,
            PricePerSquareMeter = 100,
             FloorNumber = "Партер",
            BuildingTotalFloors = 5,
            },

            new RealEstate {
            Id = "myRealEstateId3",
            RealEstateTypeId = 3,
            Address = new Address{ Description = "ул. Маргаритка 2", Id = 8},
            Price = 1000,
            Area = 100,
            PricePerSquareMeter = 10 },

            new RealEstate {Id = "myRealEstateId4",
            Price = 65000,
            Area = 65,
            PricePerSquareMeter = 1000 },
        };

        private Offer TestOffer = new Offer
        {
            Id = "coolOfferId1797",
            RealEstateId = "myRealEstateId1",
        };

        public RealEstateServiceTests()
        {
            this.context = InMemoryDatabase.GetDbContext();
            this.SeedData();
            this.realEstateTypeServices = new Mock<IRealEstateTypeServices>();
            this.citiesServices = new Mock<ICitiesServices>();
            this.neighbourhoodServices = new Mock<INeighbourhoodServices>();
            this.addressServices = new Mock<IAddressServices>();
            this.villageServices = new Mock<IVillageServices>();
            this.buildingTypeServices = new Mock<IBuildingTypeServices>();
            this.heatingSystemServices = new Mock<IHeatingSystemServices>();
        }

        [Test]
        public async Task CreateRealEstateShoulReturnRealEstateId()
        {
            string expectedId = "myNewCoolId";
            var serviceInstance = new Mock<IRealEstateServices>();

            var realEstateModel = new RealEstateCreateServiceModel()
            {
                Id = expectedId,
                Price = 5000,
                Area = 100,
                RealEstateType = "Мезонет",
                Address = "ул. Бира 2",
                FloorNumber = "4",
                BuildingTotalFloors = 5,
                City = "София",
                BuildingType = "Тухла",
            };

            serviceInstance.Setup(x => x.CreateRealEstateAsync(realEstateModel)).ReturnsAsync(expectedId);
            var service = serviceInstance.Object;
            var actualResult = await service.CreateRealEstateAsync(realEstateModel);

            Assert.That(expectedId.Equals(actualResult), InvalidReturnIdMessage);
        }

        [Test]
        public void CreateRealEstateShoulThrowsExceptionUponInvalidParameters()
        {
            string expectedId = "myNewCoolId";
            string invalidAddress = "";
            var mapper = this.GetMapper();

            var serviceInstance = new RealEstateServices(context,
                realEstateTypeServices.Object,
                citiesServices.Object,
                neighbourhoodServices.Object,
                addressServices.Object,
                villageServices.Object,
                buildingTypeServices.Object,
                heatingSystemServices.Object,
                mapper);

            var realEstateModel = new RealEstateCreateServiceModel()
            {
                Id = expectedId,
                Price = 5000,
                Area = 100,
                RealEstateType = "Мезонет",
                Address = invalidAddress,
                FloorNumber = "4",
                BuildingTotalFloors = 5,
                City = "София",
                BuildingType = "Тухла",
            };

            Assert.ThrowsAsync<ArgumentNullException>(async () => await serviceInstance.CreateRealEstateAsync(realEstateModel), InvalidAddressExceptionMessage);
        }

        [Test]
        public async Task GetRealEstateDetailsShouldReturnDetailsServiceModel()
        {
            var mapper = this.GetMapper();

            var expectedRealEstate = this.TestData.FirstOrDefault();
            var id = expectedRealEstate.Id;
            var mappedEstate = mapper.Map<RealEstateDetailsServiceModel>(expectedRealEstate);

            var serviceInstance = new Mock<IRealEstateServices>();
            serviceInstance.Setup(x => x.GetDetailsAsync(id)).ReturnsAsync(mappedEstate);
            var service = serviceInstance.Object;
            var actualResult = await service.GetDetailsAsync(id);

            Assert.IsTrue(expectedRealEstate.Area.Equals(actualResult.Area), ParameterValueMismatchMessage);
            Assert.IsTrue(expectedRealEstate.Price.Equals(actualResult.Price), ParameterValueMismatchMessage);
        }

        [Test]
        public void GetRealEstateDetailsShouldThrowAnExceptionIfNoSuchRealEstate()
        {
            var mapper = this.GetMapper();

            var invalidId = "reallyInvalidId";

            var serviceInstance = new RealEstateServices(context,
                realEstateTypeServices.Object,
                citiesServices.Object,
                neighbourhoodServices.Object,
                addressServices.Object,
                villageServices.Object,
                buildingTypeServices.Object,
                heatingSystemServices.Object,
                mapper);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await serviceInstance.GetDetailsAsync(invalidId), NonExistingRealEstateMessage);
        }

        [Test]
        public async Task EditRealEstateShouldReturnTrue()
        {
            var mapper = this.GetMapper();

            var realEstateToEdit = this.TestData.FirstOrDefault();
            var mappedRealEstate = mapper.Map<RealEstateEditServiceModel>(realEstateToEdit);

            double newArea = 666;
            decimal newPrice = 999999m;
            mappedRealEstate.Area = newArea;
            mappedRealEstate.Price = newPrice;

            var serviceInstance = new RealEstateServices(context,
                realEstateTypeServices.Object,
                citiesServices.Object,
                neighbourhoodServices.Object,
                addressServices.Object,
                villageServices.Object,
                buildingTypeServices.Object,
                heatingSystemServices.Object,
                mapper);

            var actualResult = await serviceInstance.EditRealEstateAsync(mappedRealEstate);
            
            Assert.IsTrue(actualResult);
            Assert.That(realEstateToEdit.Area == newArea);
        }

        [Test]
        public void EditRealEstateShouldThrowAnExceptionIfNoSuchRealEstate()
        {
            var mapper = this.GetMapper();

            var nonExistingRealEstate = new RealEstateEditServiceModel
            {
                Id = "Agent007",
                Price = 7500m,
                Area = 7,
            };

            var serviceInstance = new RealEstateServices(context,
                realEstateTypeServices.Object,
                citiesServices.Object,
                neighbourhoodServices.Object,
                addressServices.Object,
                villageServices.Object,
                buildingTypeServices.Object,
                heatingSystemServices.Object,
                mapper);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await serviceInstance.EditRealEstateAsync(nonExistingRealEstate), NonExistingRealEstateMessage);
        }

        [Test]
        public async Task GetRealEstateIDByOfferIdShouldReturnRealEstateId()
        {
            var offer = this.TestOffer;
            var offerId = offer.Id;
            var expectedId = offer.RealEstateId;

            var serviceInstance = new Mock<IRealEstateServices>();
            serviceInstance.Setup(x => x.GetRealEstateIdByOfferId(offerId)).ReturnsAsync(expectedId);
            var service = serviceInstance.Object;
            var actualResult = await service.GetRealEstateIdByOfferId(offerId);

            Assert.IsTrue(actualResult.Equals(expectedId));
        }

        [Test]
        [TestCase("")]
        [TestCase("invalidOfferId")]
        public void GetRealEstateIDByOfferIdShouldThrowException(string offerId)
        {
            var serviceInstance = new RealEstateServices(context,
                realEstateTypeServices.Object,
                citiesServices.Object,
                neighbourhoodServices.Object,
                addressServices.Object,
                villageServices.Object,
                buildingTypeServices.Object,
                heatingSystemServices.Object,
                mapper);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await serviceInstance.GetRealEstateIdByOfferId(offerId), NonExistingRealEstateMessage);
        }

        private IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<RealEstate, RealEstateDetailsServiceModel>();
                x.CreateMap<RealEstate, RealEstateEditServiceModel>();
                x.CreateMap<RealEstateEditServiceModel, RealEstate>()
                 .ForMember(y => y.Address, z => z.Ignore());
            });

            var mapper = new Mapper(configuration);
            return mapper;
        }

        private void SeedData()
        {
            context.RealEstates.AddRange(TestData);
            context.Offers.Add(TestOffer);
            context.SaveChanges();
        }
    }
}
