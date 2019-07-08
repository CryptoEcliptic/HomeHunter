using HomeHunter.Data;
using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

namespace Tests
{
    public class UsersServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<HomeHunterDbContext>()
                   .UseInMemoryDatabase(databaseName: "IsUserAuthenticated_Users_Database")
                   .Options;
        }
    }
}