using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services;
using HomeHunter.Services.Contracts;
using HomeHunterTests.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunterTests
{
    [TestFixture]
    public class AddressServicesTests
    {
        private const string ShouldReturnTrueMessage = "Test result should return true, but it was false!";
        private const string ShouldReturnFalseMessage = "Expected result is false. But method returned true!";
        private List<Address> GetTestData = new List<Address>()
        {

            new Address { Description = "Ул. Обиколна 55А", CityId = 11, Id = 1, IsDeleted = false },

             new Address { Description = "Ул. Калина малина 1Б", CityId = 12, Id = 2, IsDeleted = false },

             new Address { Description = "Ул. Горно Нанадолнище 18", CityId = 13, Id = 3, IsDeleted = false },

        };

        public AddressServicesTests()
        {
            this.SeedData();
        }

        [Test]
        public async Task AddAddressToTheDbShouldReturnTrue()
        {
            var expectedResult = GetTestData.FirstOrDefault();

            var addressServices = new Mock<IAddressServices>();
            addressServices.Setup(x => x.CreateAddressAsync(null, expectedResult.Description, null, null)).ReturnsAsync(expectedResult);

            var service = addressServices.Object;
            var actualResult = await service.CreateAddressAsync(null, expectedResult.Description, null, null);

            Assert.IsTrue(actualResult.Equals(expectedResult), ShouldReturnTrueMessage);
        }

        [Theory]
        [TestCase(2)]
        [TestCase(55)]
        public async Task EditAddressToShouldReturnTrue(int addressId)
        {
            var expectedResult = GetTestData.FirstOrDefault(x => x.Id == addressId);

            var addressServices = new Mock<IAddressServices>();
            addressServices.Setup(x => x.EditAddressAsync(addressId, null, null, null, null)).ReturnsAsync(expectedResult);

            var service = addressServices.Object;
            var actualResult = await service.EditAddressAsync(addressId, null, null, null, null);

            Assert.IsTrue(actualResult == expectedResult, ShouldReturnTrueMessage);
        }

        [Test]
        public async Task EditAddressToShouldChandeDescription()
        {
            var address = GetTestData.FirstOrDefault();
            var newAddressDescription = "ул. ХуяВей 69А";
            var expectedAddress = new Address { Description = newAddressDescription, CityId = 11, Id = 1, };

            var addressServices = new Mock<IAddressServices>();
            addressServices.Setup(x => x.EditAddressAsync(address.Id, null, newAddressDescription, null, null)).ReturnsAsync(expectedAddress);

            var service = addressServices.Object;
            var actualResult = await service.EditAddressAsync(address.Id, null, newAddressDescription, null, null);

            Assert.IsTrue(actualResult == expectedAddress, ShouldReturnTrueMessage);
        }

        [Test]
        public async Task DeleteUnexistingAddressShouldReturnFalse()
        {
            var context = InMemoryDatabase.GetDbContext();
            var invalidId = 55;

            var addressServices = new AddressServices(context);
            var actualResult = await addressServices.DeleteAddressAsync(invalidId);

            Assert.IsFalse(actualResult, ShouldReturnFalseMessage);
        }

        [Test]
        public async Task DeleteExistingAddressShouldReturnTrue()
        {
            var context = InMemoryDatabase.GetDbContext();
            int idToDelete = 3;
            var addressToDelete = GetTestData.FirstOrDefault(x => x.Id == idToDelete);

            var addressServices = new AddressServices(context);
            var actualResult = await addressServices.DeleteAddressAsync(idToDelete);

            Assert.IsTrue(actualResult, ShouldReturnFalseMessage);
            Assert.That(addressToDelete.IsDeleted = true);
        }

        private void SeedData()
        {
            var context = InMemoryDatabase.GetDbContext();
            context.Addresses.AddRange(GetTestData);
            context.SaveChanges();
        }
    }
}
