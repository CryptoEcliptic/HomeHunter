using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.Image;
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
    public class ImageServicesTests
    {
        private const string ClodinaryImageFolderName = "RealEstates/";
        private const string ExceptionMessage = "Exception should be thrown!";
        private const string ImageCountMismatchMessage = "Actual images count is {0}  but it is expected to be {1}!";
        private const string ExpectedTrueResultMessage = "The expected result from the test is true, but the result was false!";
        private const string DifferentImageIdMessage = "The actual image id is different from the expected one!";
        private List<Image> TestData = new List<Image>
        {
              new Image { Url = @"https://res.cloudinary.com/home-hunter-cloud/image/upload/v1566894195/RealEstates/087f784b-a79a-42ae-8782-58958345edaa.jpg" , Id = "myUniqueId1", RealEstateId = "uniqueEstateId1" },

              new Image { Url = @"https://res.cloudinary.com/home-hunter-cloud/image/upload/v1566894195/RealEstates/087f784b-a79a-42ae-8782-58958345edaa.jpg" , Id = "myUniqueId2", RealEstateId = "uniqueEstateId1" },

              new Image { Url = @"https://res.cloudinary.com/home-hunter-cloud/image/upload/v1566894195/RealEstates/087f784b-a79a-42ae-8782-58958345edaa.jpg" , Id = "myUniqueId3", RealEstateId = "uniqueEstateId2" },
        };

        private List<RealEstate> RealEstatesData = new List<RealEstate>
        {
            new RealEstate{ Price = 1000, Area = 120, Address = new Address{ Description = "Milin kamak 1", Id = 1050}, Id = "uniqueEstateId1"  },
        };

        private HomeHunterDbContext context;

        public ImageServicesTests()
        {
            this.context = InMemoryDatabase.GetDbContext();
            this.SeedData();
        }

        [Test]
        public async Task AddImageToTheDbShouldReturnTrue()
        {
            var mapper = this.GetMapper();
            var realEstateServices = new Mock<IRealEstateServices>();

            var imageServices = new ImageServices(context, mapper, realEstateServices.Object);
            string publicKey = "myCoolPublicKey";
            string url = @"https://res.cloudinary.com/home-hunter-cloud/image/upload/v1566894195/RealEstates/087f784b-a79a-42ae-8782-58958345edaa.jpg";
            string realEstateId = "myniqueRealEstateId1";
            int sequence = 1;

            var actualResult = await imageServices.AddImageAsync(publicKey, url, realEstateId, sequence);

            Assert.IsTrue(actualResult, ExpectedTrueResultMessage);
        }

        [Test]
        public void AddImageWithImvalidParametersShouldThrowAnAxception()
        {
            var mapper = this.GetMapper();
            var realEstateServices = new Mock<IRealEstateServices>();

            var imageServices = new ImageServices(context, mapper, realEstateServices.Object);
            string invalidPublicKey = null;
            string url = @"https://res.cloudinary.com/home-hunter-cloud/image/upload/v1566894195/RealEstates/087f784b-a79a-42ae-8782-58958345edaa.jpg";
            string realEstateId = "myniqueRealEstateId1";
            int sequence = 1;

            Assert.ThrowsAsync<ArgumentNullException>(() => imageServices.AddImageAsync(invalidPublicKey, url, realEstateId, sequence), ExceptionMessage);
        }

        [Test]
        public void LoadRealEstateImagesByRealEstateIdShouldReturnImages()
        {
            var realEstateId = this.TestData.Select(x => x.RealEstateId).FirstOrDefault();
            var expectedImagesCount = 2;

            var mapper = this.GetMapper();
            var realEstateServices = new Mock<IRealEstateServices>();
            var imageServices = new ImageServices(context, mapper, realEstateServices.Object);
           
            var actualResult = imageServices.LoadImagesAsync(realEstateId);
            Assert.That(actualResult.Images.Count() == expectedImagesCount, ImageCountMismatchMessage, actualResult.Images.Count(), expectedImagesCount);
            Assert.That(actualResult.Images.Select(x => x.RealEstateId).FirstOrDefault() == realEstateId);
        }

        [Test]
        public void ImagesCountShouldReturnTwo()
        {
            var realEstateId = this.TestData.Select(x => x.RealEstateId).FirstOrDefault();
            var expectedResult = 2;
            var mapper = this.GetMapper();
            var realEstateServices = new Mock<IRealEstateServices>();
            var imageServices = new ImageServices(context, mapper, realEstateServices.Object);

            var actualResult = imageServices.ImagesCount(realEstateId);

            Assert.That(expectedResult.Equals(actualResult), ImageCountMismatchMessage, actualResult, expectedResult);
        }

        [Test]
        public void ImagesCountInvalidRealEstateIdShouldThrowException()
        {
            var realEstateId = "invalidRealEstateId";
           
            var mapper = this.GetMapper();
            var realEstateServices = new Mock<IRealEstateServices>();
            var imageServices = new ImageServices(context, mapper, realEstateServices.Object);

            Assert.Throws<ArgumentException>(() => imageServices.ImagesCount(realEstateId), ExceptionMessage);
        }

       [Test]
        public async Task EditEmageShouldReturnTrue()
        {
            var mapper = this.GetMapper();
            var realEstateServices = new Mock<IRealEstateServices>();

            var imageServices = new ImageServices(context, mapper, realEstateServices.Object);

            string publicKey = "myUniqueImageKey007";
            string url = @"https://res.cloudinary.com/home-hunter-cloud/image/upload/v1566894195/RealEstates/087f784b-a79a-42ae-8782-8345edaa.jpg";
            string realEstateId = "myniqueRealEstateId2";
            bool isIndexImage = true;

            var actualResult = await imageServices.EditImageAsync(publicKey, url, realEstateId, 1);

            Assert.IsTrue(actualResult, ExpectedTrueResultMessage);
        }

        [Test]
        public void EditImageUponInvalidURLShouldThrowsException()
        {
            var mapper = this.GetMapper();
            var realEstateServices = new Mock<IRealEstateServices>();

            var imageServices = new ImageServices(context, mapper, realEstateServices.Object);

            string publicKey = "myUniqueImageKey007";
            string invalidUrl = @"";
            string realEstateId = "myniqueRealEstateId2";
          

            Assert.ThrowsAsync<ArgumentNullException>(() => imageServices.EditImageAsync(publicKey, invalidUrl, realEstateId, 2), ExceptionMessage);
        }

        [Test]
        public async Task RemoveImagesByRealEstateShouldReturnTwo()
        {
            var mapper = this.GetMapper();
            var realEstateServices = new Mock<IRealEstateServices>();

            var imageServices = new ImageServices(context, mapper, realEstateServices.Object);
            var realEstateId = this.TestData.Select(x => x.RealEstateId).FirstOrDefault();
            int expectedResult = 2;
            int actualResult = await imageServices.RemoveImages(realEstateId);

            Assert.IsTrue(actualResult.Equals(expectedResult), ImageCountMismatchMessage);
        }

        [Test]
        public void RemoveImagesSholdThrowExceptionIfRealEstateIdIsInvalid()
        {
            var mapper = this.GetMapper();
            var realEstateServices = new Mock<IRealEstateServices>();

            var imageServices = new ImageServices(context, mapper, realEstateServices.Object);
            var realEstateId = "";

            Assert.ThrowsAsync<InvalidOperationException>(() => imageServices.RemoveImages(realEstateId), ExceptionMessage);
        }

        [Test]
        public async Task GetImageIdsShouldReturnTwoImageIds()
        {
            var mapper = this.GetMapper();
            var realEstateServices = new Mock<IRealEstateServices>();

            var imageServices = new ImageServices(context, mapper, realEstateServices.Object);
            var realEstateId = this.TestData.Select(x => x.RealEstateId).FirstOrDefault();

            var imageIds = await imageServices.GetImageIds(realEstateId);

            var expectedImageIds = this.TestData
                .Select(x => ClodinaryImageFolderName + x.Id)
                .ToList();

            foreach (var imadeId in imageIds)
            {
                Assert.That(expectedImageIds.Any(x => x == imadeId), DifferentImageIdMessage);
            }

        }

        [Test]
        public void GetImageIDsSholdThrowExceptionIfRealEstateIdIsInvalid()
        {
            var mapper = this.GetMapper();
            var realEstateServices = new Mock<IRealEstateServices>();

            var imageServices = new ImageServices(context, mapper, realEstateServices.Object);
            var realEstateId = "";

            Assert.ThrowsAsync<ArgumentNullException>(() => imageServices.GetImageIds(realEstateId), ExceptionMessage);
        }

        private void SeedData()
        {
            context.Images.AddRange(TestData);
            context.RealEstates.AddRange(RealEstatesData);
            context.SaveChanges();
        }

        private IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<Image, ImageChangeableServiceModel>();
                x.CreateMap<ImageChangeableServiceModel, ImageLoadServiceModel>();
            });

            var mapper = new Mapper(configuration);
            return mapper;
        }

    }
}
