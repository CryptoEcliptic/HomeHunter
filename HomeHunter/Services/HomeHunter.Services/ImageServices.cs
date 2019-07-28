using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.Image;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class ImageServices : IImageServices
    {
        private readonly HomeHunterDbContext context;
        private readonly IMapper mapper;

        public ImageServices(HomeHunterDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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

        public ImageLoadServiceModel LoadImagesAsync(string offerId)
        {
            var realEstateId = this.context.Offers
                .FirstOrDefault(x => x.Id == offerId)
                .RealEstateId;

            var images = this.context.Images
                .Where(x => x.RealEstateId == realEstateId)
                .ToList();

            var imageDelitableServiceModel = this.mapper.Map<List<DelitableImageServiceModel>>(images);

            ImageLoadServiceModel imageLoadServiceModel = new ImageLoadServiceModel();

            foreach (var image in imageDelitableServiceModel)
            {
                imageLoadServiceModel.DelitableImages.Add(image);
            }
            return imageLoadServiceModel;
        }
    }
}
