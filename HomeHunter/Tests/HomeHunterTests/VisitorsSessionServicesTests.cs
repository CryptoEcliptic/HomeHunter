using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services;
using HomeHunterTests.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunterTests
{
    [TestFixture]
    public class VisitorsSessionServicesTests
    {
        private const string ExpectedTrueTestResultMessage = "The expected test result should be true, but it was false!";

        private List<VisitorSession> TestData = new List<VisitorSession>
        {
            new VisitorSession
            {
                Id = "uniqueSession1",
                IpAddress = "192.927.0.0.1",
                VisitorId = Guid.NewGuid().ToString(),
            },

             new VisitorSession
             {
                Id = "uniqueSession2",
                IpAddress = "192.927.0.0.5",
                VisitorId = Guid.NewGuid().ToString(),
            }
        };

        private readonly HomeHunterDbContext context;

        public VisitorsSessionServicesTests()
        {
            this.context = InMemoryDatabase.GetDbContext();
            this.SeedData();

        }

        [Test]
        public async Task AddSessionInTheDb()
        {
            var serviceInstance = new VisitorSessionServices(context);

            string visitorId = Guid.NewGuid().ToString();
            var ipAddress = "192.927.0.0.1";

            var actualResult = await serviceInstance.AddSessionInTheDb(ipAddress, visitorId);

            Assert.IsTrue(actualResult, ExpectedTrueTestResultMessage);
        }

        [Test]
        public async Task AddSessionInTheDbReturnFalseUponInvalidParameter()
        {
            var serviceInstance = new VisitorSessionServices(context);

            string visitorId = Guid.NewGuid().ToString();
            var invalidIpAdddress = "";

            var actualResult = await serviceInstance.AddSessionInTheDb(invalidIpAdddress, visitorId);

            Assert.IsFalse(actualResult);
        }

        [Test]
        public async Task VisitorsCountShouldReturnThreeOrTwo()
        {
            var serviceInstance = new VisitorSessionServices(context);
            var actualResult = await serviceInstance.UniqueVisitorsCount();

            var expectedResult = 3;
            var expecredResultForRunningOnlyThisTest = 2;

            Assert.IsTrue(actualResult.Equals(expectedResult) || actualResult.Equals(expecredResultForRunningOnlyThisTest), ExpectedTrueTestResultMessage);
        }

        private void SeedData()
        {
            context.VisitorsSessions.AddRange(TestData);
            context.SaveChanges();
        }
    }
}
