using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class ImageServices : IImageServices
    {
        private readonly HomeHunterDbContext context;

        public ImageServices(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddImageAsync(string url, string estateId)
        {
            if (url == null || estateId == null)
            {
                return false;
            }

            var image = new Image
            {
                Url = url,
                RealEstateId = estateId,
            };

            await this.context.Images.AddAsync(image);
            await this.context.SaveChangesAsync();

            return true;
        }
    }
}
