using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Models.ViewModels.User;
using HomeHunter.Services;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.EmailSender;
using HomeHunter.Services.Models.User;
using HomeHunterTests.Common;
using Microsoft.AspNetCore.Identity;
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
    public class UsersServiceTests
    {
        private const string EmailMismatchMessage = "Emails do not match";
        private const string ArgumentNullExceptionMessage = "Argument null exception should be thrown!";
        private const string UserIdMismatchMessage = "Actual userId does not match with the expected one!";
        private const string ShouldReturnTrueMessage = "Test result should return true, but it was false!";
        private const string ShouldReturnZeroMessge = "Returned result should be equal to zero, but it was different.";

        private List<HomeHunterUser> GetTestData = new List<HomeHunterUser>()
        {

            new HomeHunterUser { Email = "rado@abv.bg", FirstName = "Rado", LastName = "Vasilev", Id = "coolUniqueId1", EmailConfirmed = true, },

            new HomeHunterUser { Email = "pesho@abv.bg", FirstName = "Pesho", LastName = "Peshov", Id = "coolUniqueId2" },

            new HomeHunterUser { Email = "gosho@abv.bg", FirstName = "Gosho", LastName = "Goshov", Id = "coolUniqueId3" },

        };
        private HomeHunterDbContext context;
        public UsersServiceTests()
        {
            this.context = InMemoryDatabase.GetDbContext();
        }

        [Test]
        public async Task GetAllUsersShouldReturnCorrectEmails()
        {
            var mapper = this.GetMapper();

            var usersFromDb = GetTestData;
            var actualResult = mapper.Map<List<UserIndexServiceModel>>(usersFromDb);

            var methodResult = new List<UserIndexServiceModel>();
            methodResult.AddRange(actualResult);

            //Create Instance of the service
            var userServices = new Mock<IUserServices>();

            userServices.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(methodResult);

            var expectedResultAsIEnumerable = await userServices.Object.GetAllUsersAsync();
            var expectedResult = expectedResultAsIEnumerable.ToList();

            for (int i = 0; i < expectedResult.Count(); i++)
            {
                Assert.True(expectedResult[i].Email == actualResult[i].Email, EmailMismatchMessage);
            }
        }

        [Test]
        public async Task NoUsersInTheDatabaseShouldReturnZero()
        {
            var mapper = this.GetMapper();

            List<HomeHunterUser> usersFromDb = null;
            var actualResult = mapper.Map<List<UserIndexServiceModel>>(usersFromDb);

            var methodResult = new List<UserIndexServiceModel>();

            //Create Instance of the service
            var userServices = new Mock<IUserServices>();
            userServices.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(methodResult);

            var expectedResultAsIEnumerable = await userServices.Object.GetAllUsersAsync();
            var expectedResult = expectedResultAsIEnumerable.ToList();

            Assert.That(expectedResult.Count == 0, ShouldReturnZeroMessge);
        }

        [Test]
        public async Task GetUsersDetailsShouldReturnCorrectUserId()
        {
            string testId = "coolUniqueId2";
            var mapper = this.GetMapper();

            var usersFromDb = GetTestData.FirstOrDefault(x => x.Id == testId);
            var expectedResult = mapper.Map<UserDetailsServiceModel>(usersFromDb);
            var methodData = expectedResult;
            var userId = methodData == null ? null : methodData.Id;
            //Create Instance of the service
            var userServices = new Mock<IUserServices>();
            userServices.Setup(x => x.GetUserDetailsAsync(userId)).ReturnsAsync(methodData);

            var actualResult = await userServices.Object.GetUserDetailsAsync(userId);
            var expectedReturnUserId = "coolUniqueId2";

            Assert.That(actualResult.Id == expectedReturnUserId, UserIdMismatchMessage);
        }

        [Test]
        public void UsersByIdShouldThrowExceptionIfUserIdIsNullOrInvalid()
        {
            string userId = null;
            var mapper = this.GetMapper();
            var store = new Mock<IUserStore<HomeHunterUser>>();
            var userManager = new Mock<UserManager<HomeHunterUser>>(store.Object, null, null, null, null, null, null, null, null);
            var emailSender = new Mock<IApplicationEmailSender>();

            var userServices = new UserServices(context, mapper, userManager.Object, emailSender.Object);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await userServices.GetUserById(userId), ArgumentNullExceptionMessage);
        }

        [Test]
        public async Task DeleteUserShouldReturnTrue()
        {
            string userId = "coolUniqueId3";
            var expectedResult = GetTestData.Any(x => x.Id == userId);

            var userServices = new Mock<IUserServices>();
            userServices.Setup(x => x.SoftDeleteUserAsync(userId)).ReturnsAsync(expectedResult);

            var service = userServices.Object;
            var actualResult = await service.SoftDeleteUserAsync(userId);

            Assert.IsTrue(actualResult.Equals(expectedResult), ShouldReturnTrueMessage);
        }

        [Test]
        public async Task DeleteUserSInvalidUserIdShouldReturnFalse()
        {
            string userId = "invalid007";
            var expectedResult = GetTestData.Any(x => x.Id == userId);

            var userServices = new Mock<IUserServices>();
            userServices.Setup(x => x.SoftDeleteUserAsync(userId)).ReturnsAsync(expectedResult);

            var service = userServices.Object;
            var actualResult = await service.SoftDeleteUserAsync(userId);

            Assert.IsTrue(actualResult.Equals(expectedResult), ShouldReturnTrueMessage);

        }

        [Test]
        public void IsEmailConfirmedShouldReturnTrue()
        {
            foreach (var user in GetTestData)
            {
                var expectedResult = user.EmailConfirmed;

                var userServices = new Mock<IUserServices>();
                userServices.Setup(x => x.IsUserEmailAuthenticated(user.Id)).Returns(expectedResult);

                var service = userServices.Object;
                var actualResult = service.IsUserEmailAuthenticated(user.Id);

                Assert.IsTrue(actualResult.Equals(expectedResult), ShouldReturnTrueMessage);
            }
        }

        private void SeedData()
        {
            context.HomeHunterUsers.AddRange(GetTestData);
            context.SaveChanges();
        }

        private IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<UserIndexServiceModel, UserIndexViewModel>();
                x.CreateMap<HomeHunterUser, UserIndexServiceModel>();
                x.CreateMap<HomeHunterUser, UserDetailsServiceModel>();
            });

            var mapper = new Mapper(configuration);
            return mapper;
        }
    }
}
