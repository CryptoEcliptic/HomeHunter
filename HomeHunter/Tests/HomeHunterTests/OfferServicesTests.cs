using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Domain.Enums;
using HomeHunter.Services;
using HomeHunter.Services.CloudinaryServices;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Helpers;
using HomeHunter.Services.Models.Offer;
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
    public class OfferServicesTests
    {
        private const string ExpectedTrueTestResultMessage = "The expected test result should be true, but it was false!";
        private const string ArgumentNullExceptonMessage = "ArgumentNull exception should have been thrown due to invalid method parameter.";
        private List<Offer> TestData = new List<Offer>
        {
            new Offer { Id = "offerId111", RealEstateId = "myRealEstateId1",
                OfferType = OfferType.Sale, AuthorId = "coolUniqueId1",
                AgentName = "Pesho",
                OfferServiceInformation = "Some Owner telephone number 012345678",
                IsDeleted = false },

            new Offer { Id = "offerId112", RealEstateId = "myRealEstateId2", OfferType = OfferType.Sale, AuthorId = "coolUniqueId2", IsDeleted = false},

            new Offer { Id = "offerId113", RealEstateId = "myRealEstateId3", OfferType = OfferType.Rental, AuthorId = "coolUniqueId3", IsDeleted = true },
        };

        private HomeHunterDbContext context;
        private readonly Mock<IImageServices> imageServices;
        private readonly Mock<ICloudinaryService> cloudinaryServices;
        private readonly Mock<IUserServices> userServices;
        private readonly Mock<IReferenceNumberGenerator> referenceNumberGenerator;
        private readonly Mock<IRealEstateServices> realEstateServices;
        private readonly IMapper mapper;

        public OfferServicesTests()
        {
            this.context = InMemoryDatabase.GetDbContext();
            this.mapper = this.GetMapper();
            this.imageServices = new Mock<IImageServices>();
            this.cloudinaryServices = new Mock<ICloudinaryService>();
            this.userServices = new Mock<IUserServices>();
            this.realEstateServices = new Mock<IRealEstateServices>();
            this.referenceNumberGenerator = new Mock<IReferenceNumberGenerator>();
            this.SeedData();
        }

        [Test]
        public async Task CreateOfferShoulReturnTrue()
        {
            var serviceInstance = new OfferServices(context, 
                imageServices.Object, 
                cloudinaryServices.Object,
                userServices.Object,
                referenceNumberGenerator.Object,
                mapper
                );

            string realEstateId = "myRealEstateId4";
            string authorId = "coolUniqueId3";
            var offerToAdd = new OfferCreateServiceModel
            {
                OfferType = "Продажба",
                Comments = "Some important comments",
                ContactNumber = "0888607796",
                AgentName = "Pesho",
                OfferServiceInformation = "Some Owner telephone number 012345678"
            };

            var actualResult = await serviceInstance.CreateOfferAsync(authorId, realEstateId, offerToAdd);

            Assert.IsTrue(actualResult, ExpectedTrueTestResultMessage);
        }

        [Test]
        public void CreateOfferShoulThrowWexeptionIfRealEstateIdIsInvalid()
        {
            var serviceInstance = new OfferServices(context,
                imageServices.Object,
                cloudinaryServices.Object,
                userServices.Object,
                referenceNumberGenerator.Object,
                mapper
                );

            string invalidRealEstateId = "";
            string authorId = "coolUniqueId3";
            var offerToAdd = new OfferCreateServiceModel
            {
                OfferType = "Продажба",
                Comments = "Some important comments",
                ContactNumber = "0888607796",
            };

            Assert.ThrowsAsync<ArgumentNullException>(async () => await serviceInstance.CreateOfferAsync(authorId, invalidRealEstateId, offerToAdd), ArgumentNullExceptonMessage);
        }


        [Test]
        public async Task GetOfferByIdShouldReturnServiceModel()
        {
            var offerToGet = this.TestData.FirstOrDefault();
            string offerId = offerToGet.Id;

            var serviceInstance = new OfferServices(context,
                imageServices.Object,
                cloudinaryServices.Object,
                userServices.Object,
                referenceNumberGenerator.Object,
                mapper
                );

            var returnedOffers = await serviceInstance.GetOfferByIdAsync(offerId);
          
            Assert.IsTrue(returnedOffers.Id.Equals(offerId), ExpectedTrueTestResultMessage);
            Assert.IsTrue(returnedOffers.OfferType.Equals(offerToGet.OfferType.ToString()), ExpectedTrueTestResultMessage);
        }

        [Test]
        public void GetOfferByIdShouldThrowExceptionIfNoSuchOffer()
        {
            string offerId = "completelyInvalidId";

            var serviceInstance = new OfferServices(context,
                imageServices.Object,
                cloudinaryServices.Object,
                userServices.Object,
                referenceNumberGenerator.Object,
                mapper
                );

            Assert.ThrowsAsync<ArgumentNullException>(() => serviceInstance.GetOfferByIdAsync(offerId));
        }

        [Test]
        public async Task EditOfferShouldReturnTrue()
        {
            var mapper = this.GetMapper();

            var offerToEdit = this.TestData.FirstOrDefault();
            var mappedOffer = mapper.Map<OfferEditServiceModel>(offerToEdit);

            string comment = "This brandly new comment";
            string agentName = "Rado";

            mappedOffer.Comments = comment;
            mappedOffer.AgentName = agentName;

            var serviceInstance = new OfferServices(context,
                imageServices.Object,
                cloudinaryServices.Object,
                userServices.Object,
                referenceNumberGenerator.Object,
                mapper
                );

            var actualResult = await serviceInstance.EditOfferAsync(mappedOffer);
 
            Assert.IsTrue(actualResult);
            Assert.That(offerToEdit.Comments == comment && offerToEdit.AgentName == agentName);
        }

        [Test]
        public void EditOfferIdShouldThrowExceptionIfNoSuchOffer()
        {
            string invalidId = "completelyInvalidId";
            var unexistingOfferModel = new OfferEditServiceModel
            {
                Id = invalidId,
                Comments = "Some comments",
                OfferType = "Sale",
            };
            

            var serviceInstance = new OfferServices(context,
                imageServices.Object,
                cloudinaryServices.Object,
                userServices.Object,
                referenceNumberGenerator.Object,
                mapper
                );

            Assert.ThrowsAsync<ArgumentNullException>(() => serviceInstance.EditOfferAsync(unexistingOfferModel), ArgumentNullExceptonMessage);
        }

        [Test]
        public void GetOfferIdByRealEstateIdShouldThrowExceptionUponInvalidParameterPassed()
        {
            var realEstateId = "";

            var serviceInstance = new OfferServices(context,
                imageServices.Object,
                cloudinaryServices.Object,
                userServices.Object,
                referenceNumberGenerator.Object,
                mapper
                );

            Assert.ThrowsAsync<ArgumentNullException>( async () => serviceInstance.GetOfferIdByRealEstateIdAsync(realEstateId), ArgumentNullExceptonMessage);
        }

        private void SeedData()
        {
            context.Offers.AddRange(TestData);
            context.SaveChanges();
        }

        private IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<Offer, OfferIndexServiceModel>();
                x.CreateMap<Offer, OfferPlainDetailsServiceModel>();
                x.CreateMap<Offer, OfferEditServiceModel>();
                x.CreateMap<OfferEditServiceModel, Offer>();
                x.CreateMap<OfferCreateServiceModel, Offer>()
                    .ForMember(z => z.OfferType, y => y.Ignore())
                    .ForMember(z => z.Author, y => y.Ignore())
                    .ForMember(z => z.ReferenceNumber, y => y.Ignore())
                    .ForMember(z => z.RealEstate, y => y.Ignore())
                    .ForMember(z => z.RealEstateId, y => y.Ignore())
                    .ForMember(z => z.Author, y => y.Ignore())
                    .ForMember(z => z.AuthorId, y => y.Ignore());          

            });

            var mapper = new Mapper(configuration);
            return mapper;
        }

    }
}
