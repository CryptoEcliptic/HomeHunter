using AutoMapper;
using HomeHunter.Domain;
using HomeHunter.Models.ViewModels.User;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.User;
using HomeHunter.Tsets.Common;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Tests
{
    public class UsersServiceTests
    {
        private const string ResultCountMismatchMessage = "Expected test result should be 3";

        private List<HomeHunterUser> GetTestData = new List<HomeHunterUser>()
        {

            new HomeHunterUser { Email = "rado@abv.bg", FirstName = "Rado", LastName = "Vasilev", Id = "coolUniqueId1" },

            new HomeHunterUser { Email = "pesho@abv.bg", FirstName = "Pesho", LastName = "Peshov", Id = "coolUniqueId2" },

            new HomeHunterUser { Email = "gosho@abv.bg", FirstName = "Gosho", LastName = "Goshov", Id = "coolUniqueId3" },
        };

        public UsersServiceTests()
        {
            this.SeedData();
        }

        [Test]
        public async Task GetUserByIdShouldReturnCorrectUserEmail()
        {
            var context = InMemoryDatabase.GetDbContext();
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
                Assert.True(expectedResult[i].Email == actualResult[i].Email);
            }
        }


        private void SeedData()
        {
            var context = InMemoryDatabase.GetDbContext();
            context.HomeHunterUsers.AddRange(GetTestData);
            context.SaveChanges();
        }

        private IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<UserIndexServiceModel, UserIndexViewModel>();
                x.CreateMap<HomeHunterUser, UserIndexServiceModel>();
            });

            var mapper = new Mapper(configuration);
            return mapper;
        }


    }
}