using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(HomeHunter.App.Areas.Identity.IdentityHostingStartup))]
namespace HomeHunter.App.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}