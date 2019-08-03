using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.Image;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class ImageServices : IImageServices
    {
        private const string ClodinaryImageFolderName = "RealEstates/";
        private const string InvalidImageParamsMessage = "Null image parameters!";

        private readonly HomeHunterDbContext context;
        private readonly IMapper mapper;
        private readonly IRealEstateServices realEstateServices;

        public ImageServices(HomeHunterDbContext context, IMapper mapper, IRealEstateServices realEstateServices)
        {
            this.context = context;
            this.mapper = mapper;
            this.realEstateServices = realEstateServices;
        }

        public async Task<bool> AddImageAsync(string publikKey, string url, string estateId)
        {
            if (url == null || estateId == null || publikKey == null)
            {
                throw new ArgumentNullException(InvalidImageParamsMessage);
            }

            var image = new Image
            {
                Url = url,
                RealEstateId = estateId,
                Id = publikKey,
            };

            await this.context.Images.AddAsync(image);
            await this.context.SaveChangesAsync();

            return true;
        }

        public ImageLoadServiceModel LoadImagesAsync(string realEstateId)
        {
            var images = this.context.Images
                .Where(x => x.RealEstateId == realEstateId)
                .ToList();

            var imageDelitableServiceModel = this.mapper.Map<List<ImageChangeableServiceModel>>(images);

            ImageLoadServiceModel imageLoadServiceModel = new ImageLoadServiceModel();

            foreach (var image in imageDelitableServiceModel)
            {
                imageLoadServiceModel.Images.Add(image);
            }

            return imageLoadServiceModel;
        }

        public async Task<ImageUploadEditServiceModel> GetImageDetailsAsync(string offerId)
        {
            var realEstateId = await this.realEstateServices.GetRealEstateIdByOfferId(offerId);

            var imagesCount = this.ImagesCount(realEstateId);

            var imageUploadEditServiceModel = new ImageUploadEditServiceModel
            {
                RealEstateId = realEstateId,
                AlreadyUploadedImagesCount = imagesCount,
            };

            return imageUploadEditServiceModel;
        }

        public int ImagesCount(string id)
        {
            return this.context.Images
            .Where(x => x.RealEstateId == id)
            .Count();
        }

        public async Task<bool> EditImageAsync(string publicKey, string url, string estateId)
        {
            if (url == null || estateId == null || publicKey == null)
            {
                throw new ArgumentNullException(InvalidImageParamsMessage);
            }

            var realEstate = this.context.RealEstates.FirstOrDefault(x => x.Id == estateId);
            
            var image = new Image
            {
                Url = url,
                RealEstateId = estateId,
                Id = publicKey,
            };

            await this.context.Images.AddAsync(image);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<int> RemoveImages(string estateId)
        {
            var realEstateImages = this.context
                .Images
                .Where(x => x.RealEstateId == estateId)
                .ToList();
            int affectedRows = 0;

            if (realEstateImages.Count != 0)
            {
                try
                {
                    this.context.Images.RemoveRange(realEstateImages);
                    affectedRows = await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return 0;
                }
            }
            return affectedRows;
        }

        public async Task<IEnumerable<string>> GetImageIds(string realEstateId)
        {
            var imageIds = await this.context.Images
                .Where(x => x.RealEstateId == realEstateId)
                .Select(x => ClodinaryImageFolderName + x.Id)
                .ToListAsync();
                
            return imageIds;
        }
    }
}
