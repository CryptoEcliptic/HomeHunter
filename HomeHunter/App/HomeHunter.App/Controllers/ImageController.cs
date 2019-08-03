using AutoMapper;
using HomeHunter.Common;
using HomeHunter.Infrastructure.CloudinaryServices;
using HomeHunter.Models.BindingModels.Image;
using HomeHunter.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HomeHunter.App.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IImageServices imageServices;
        private readonly IRealEstateServices realEstateServices;
        private readonly IMapper mapper;

        public ImageController(ICloudinaryService cloudinaryService,
            IImageServices imageServices,
            IRealEstateServices realEstateServices,
            IMapper mapper)
        {
            this.cloudinaryService = cloudinaryService;
            this.imageServices = imageServices;
            this.realEstateServices = realEstateServices;
            this.mapper = mapper;
        }

        [HttpGet("/Image/Upload/{id}")]
        public async Task<IActionResult> Upload(string id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(string id, ImageUploadBindingModel model)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("Error", "Home");
            }

            if (this.imageServices.ImagesCount(id) > GlobalConstants.ImageUploadLimit)
            {
                return RedirectToAction("Error", "Home");
            }

            foreach (var image in model.Images)
            {
                var imageId = Guid.NewGuid().ToString();

                try
                {
                    var imageUrl = await this.cloudinaryService.UploadPictureAsync(image, imageId);
                    var isImageAddedInDb = await this.imageServices.AddImageAsync(imageId, imageUrl, id);

                }
                catch (FormatException)
                {
                    return RedirectToAction("Error", "Home");
                }
                catch (ArgumentNullException)
                {

                    return RedirectToAction("Error", "Home");
                }

            }

            RedirectToActionResult redirectResult = new RedirectToActionResult("Create", "Offer", new { @Id = $"{id}" });
            return redirectResult;
        }

        [HttpGet("/Image/Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var imageUploadEditServiceModel = await this.imageServices.GetImageDetailsAsync(id);
            var imageUploadEditBindingModel = this.mapper.Map<ImageUploadEditBindingModel>(imageUploadEditServiceModel);

            return View(imageUploadEditBindingModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ImageUploadEditBindingModel model)
        {
            if (string.IsNullOrWhiteSpace(id)) //OfferId
            {
                return RedirectToAction("Error", "Home");
            }

            var realEstateId = await this.realEstateServices.GetRealEstateIdByOfferId(id);
            
            if (model.Images.Count != 0)
            {
                var imageIdsToDelete = await this.imageServices.GetImageIds(realEstateId);

                int removedImagesFromCloudinary = await this.cloudinaryService.DeleteCloudinaryImages(imageIdsToDelete);
                var hasOldImagesBeenRemoved = await this.imageServices.RemoveImages(realEstateId);

                foreach (var image in model.Images)
                {
                    var imageId = Guid.NewGuid().ToString();

                    try
                    {
                        var imageUrl = await this.cloudinaryService.UploadPictureAsync(image, imageId);
                        var isImageAddedInDb = await this.imageServices.EditImageAsync(imageId, imageUrl, realEstateId);
                    }
                    catch (FormatException)
                    {

                        return RedirectToAction("Error", "Home");
                    }
                    catch (ArgumentNullException)
                    {

                        return RedirectToAction("Error", "Home");
                    }
                }
            }
          
            RedirectToActionResult redirectResult = new RedirectToActionResult("Edit", "RealEstates", new { @Id = $"{realEstateId}" });
            return redirectResult;
        }
    }
}