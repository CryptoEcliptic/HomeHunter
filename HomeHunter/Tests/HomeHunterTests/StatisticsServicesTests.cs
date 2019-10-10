using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.User;
using HomeHunterTests.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunterTests
{
    [TestFixture]
    public class StatisticsServicesTests
    {
        private HomeHunterDbContext context;

        private List<Offer> testOffers = new List<Offer>
        {
             new Offer {RealEstateId = "myRealEstateId100",
             OfferType = HomeHunter.Domain.Enums.OfferType.Sale, IsOfferActive = false, IsDeleted = false, },

             new Offer {RealEstateId = "myRealEstateId200",
             OfferType = HomeHunter.Domain.Enums.OfferType.Sale, IsOfferActive = true, IsDeleted = false,  },

             new Offer {RealEstateId = "myRealEstateId300",
             OfferType = HomeHunter.Domain.Enums.OfferType.Rental, IsOfferActive = true, IsDeleted = false,  },

              new Offer {RealEstateId = "myRealEstateId400",
             OfferType = HomeHunter.Domain.Enums.OfferType.Sale, IsOfferActive = true, IsDeleted = false,  },
        };

        private List<RealEstate> testRealEsatates = new List<RealEstate>
        {
            new RealEstate {Id = "myRealEstateId100",
            Price = 10000,
            Area = 500,
            PricePerSquareMeter = 200 },

            new RealEstate {Id = "myRealEstateId200",
            Price = 50000,
            Area = 500,
            PricePerSquareMeter = 100 },

            new RealEstate {Id = "myRealEstateId300",
            Price = 1000,
            Area = 100,
            PricePerSquareMeter = 10 },

            new RealEstate {Id = "myRealEstateId400",
            Price = 65000,
            Area = 65,
            PricePerSquareMeter = 1000 },

        };

      
        public StatisticsServicesTests()
        {
            this.context = this.GetDbContext();
            this.SeedData();  
        }

        [Test]
        public async Task GetActiveOffersCountShouldReturnTrue()
        {
            var userServices = new Mock<IUserServices>();
            var visitorSessionServices = new Mock<IVisitorSessionServices>();

            var statisticServices = new StatisticServices(context, userServices.Object, visitorSessionServices.Object);
            var actualResult = await statisticServices.GetAdministrationStatistics();
            var ecpectedOffersCount = 3;

            Assert.IsTrue(actualResult.ActiveOffersCount == ecpectedOffersCount);
        }

        [Test]
        public async Task GetInactiveOffersCountShouldReturnTrue()
        {
            var userServices = new Mock<IUserServices>();
            var visitorSessionServices = new Mock<IVisitorSessionServices>();

            var statisticServices = new StatisticServices(context, userServices.Object, visitorSessionServices.Object);
            var actualResult = await statisticServices.GetAdministrationStatistics();
            var ecpectedOffersCount = 1;

            Assert.IsTrue(actualResult.DeactivatedOffersCount == ecpectedOffersCount);
        }

        [Test]
        public async Task GetActiveRentalsOffersCountShouldReturnTrue()
        {
            var userServices = new Mock<IUserServices>();
            var visitorSessionServices = new Mock<IVisitorSessionServices>();

            var statisticServices = new StatisticServices(context, userServices.Object, visitorSessionServices.Object);
            var actualResult = await statisticServices.GetAdministrationStatistics();
            var ecpectedOffersCount = 1;

            Assert.IsTrue(actualResult.ActiveRentalsCount == ecpectedOffersCount);
        }

        private void SeedData()
        {
            context.RealEstates.AddRange(testRealEsatates);
            context.Offers.AddRange(testOffers);
            context.SaveChanges();
        }

        public HomeHunterDbContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HomeHunterDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new HomeHunterDbContext(optionsBuilder.Options);

            return context;
        }
    }
}
