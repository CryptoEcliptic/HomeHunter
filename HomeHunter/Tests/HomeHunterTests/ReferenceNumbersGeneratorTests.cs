using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Domain.Enums;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Helpers;
using HomeHunterTests.Common;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunterTests
{
    [TestFixture]
    public class ReferenceNumbersGeneratorTests
    {
        
        private const string ExpectedTrueResultMessage = "Expected result should return {0}, but it is {1}!";

        private readonly List<RealEstate> TestData = new List<RealEstate>
        {
            new RealEstate {
            Id = "trex1",
            RealEstateType = new RealEstateType{ TypeName = "Едностаен апартамент", Id = 1000, MinReferenceNumber = "0001" },
            },

            new RealEstate {
            Id = "trex2",
            RealEstateType = new RealEstateType{ TypeName = "Двустаен апартамент", Id = 2000, MinReferenceNumber = "0200"},
            },

            new RealEstate {
            Id = "trex3",
            RealEstateType = new RealEstateType{ TypeName = "Мезонет", Id = 3000, MinReferenceNumber = "0600"},
            },

            new RealEstate {
            Id = "trex4",
            RealEstateType = new RealEstateType{ TypeName = "Тристаен апартамент", Id = 4000, MinReferenceNumber = "0300", MaxReferenceNumber = "0399"}, Offer = new Offer{ Id="852315613", ReferenceNumber = "300399", OfferType = OfferType.Sale},
            },

            new RealEstate {
            Id = "trex5",
            RealEstateType = new RealEstateType{ TypeName = "Тристаен апартамент", Id = 4000, MinReferenceNumber = "0300", MaxReferenceNumber = "0399"}
            },
        };

        private readonly HomeHunterDbContext context;

        public ReferenceNumbersGeneratorTests()
        {
            this.context = InMemoryDatabase.GetDbContext();
            this.SeedData();
        }

        [Test]
        [TestCase("Продажба", "trex1", "300001")]
        [TestCase("Продажба", "trex2", "300200")]
        [TestCase("Наем", "trex3", "100600")]
        public async Task CreateReferenceNumberForAppartmentsShouldReturnTrue(string offerType, string realEstateId, string expectedResult)
        {
            var referenceNumberGener = new ReferenceNumberGenerator(this.context);
            var actualResult = await referenceNumberGener.GenerateOfferReferenceNumber(offerType, realEstateId);

            Assert.That(expectedResult.Equals(actualResult), ExpectedTrueResultMessage, expectedResult, actualResult);
        }

        [Test]
        [TestCase("Продажба", "trex5", "300300")]
        public async Task CreateReferenceNumberWhenMaxValueIsReachedShouldRetrunTrue(string offerType, string realEstateId, string expectedResult)
        {
            var referenceNumberGener = new ReferenceNumberGenerator(this.context);
            var actualResult = await referenceNumberGener.GenerateOfferReferenceNumber(offerType, realEstateId);

            Assert.That(expectedResult.Equals(actualResult), ExpectedTrueResultMessage, expectedResult, actualResult);
        }

        private void SeedData()
        {
            context.RealEstates.AddRange(TestData);
            context.SaveChanges();
        }
    }
}

