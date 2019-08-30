using HomeHunter.App;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunterTests
{
    [TestFixture]
    public class IntegrationTests
    {
        private WebApplicationFactory<Startup> server;
        private HttpClient client;

        [SetUp]
        public void Initialiser()
        {
            this.server = new WebApplicationFactory<Startup>();
            this.client = server.CreateClient();
        }

        [Test]
        public async Task OfferIndexSalesPageShouldReturn200OK()
        {
            var expectedHtml = "<h2>Продажби</h2>";
            var testAddress = "/Offer/IndexSales";

            var response = await client.GetAsync(testAddress);
            var html = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            StringAssert.Contains(expectedHtml, html);
        }

        [Test]
        public async Task OfferIndexRentalsPageShouldReturn200OK()
        {
            var expectedHtml = "<h2>Наеми</h2>";
            var testAddress = "/Offer/IndexRentals";

            var response = await client.GetAsync(testAddress);
            var html = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            StringAssert.Contains(expectedHtml, html);
        }
    }
}
