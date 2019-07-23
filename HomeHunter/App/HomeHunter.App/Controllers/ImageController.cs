using HomeHunter.Infrastructure.CloudinaryServices;
using HomeHunter.Models.BindingModels.Image;
using HomeHunter.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HomeHunter.App.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IImageServices imageServices;

        public ImageController(ICloudinaryService cloudinaryService, IImageServices imageServices)
        {
            this.cloudinaryService = cloudinaryService;
            this.imageServices = imageServices;
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
                return NotFound();
            }

            foreach (var image in model.Images)
            {
                var imageId = Guid.NewGuid().ToString();
                var imageRrl = await this.cloudinaryService.UploadPictureAsync(image, imageId);

                var isImageAddedInDb = await this.imageServices.AddImageAsync(imageRrl, id);

                if (!isImageAddedInDb)
                {
                    throw new ArgumentNullException("Invalid Db input params!");
                };
            }

            RedirectToActionResult redirectResult = new RedirectToActionResult("Upload", "Image", new { @Id = $"{id}" });
            return redirectResult;
        }

    }
}