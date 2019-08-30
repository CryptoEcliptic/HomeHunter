using HomeHunter.App;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunterTests
{
    [TestFixture]
    public class OfferServicesIntegrationTests 
    {
        private WebApplicationFactory<Startup> server;

        [SetUp]
        public void Initialiser()
        {
            this.server = new WebApplicationFactory<Startup>();
        }


        [Test]
        public async Task IndexPageShouldReturn200OK()
        {
            var client = server.CreateClient();
            var response = await client.GetAsync("/Offer/Index");
            response.EnsureSuccessStatusCode();
        }
    }
}
