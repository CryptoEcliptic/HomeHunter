using AutoMapper;
using HomeHunter.Infrastructure.CloudinaryServices;
using HomeHunter.Models.BindingModels.Image;
using HomeHunter.Models.ViewModels.Image;
using HomeHunter.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.App.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly IImageServices imageServices;
        private readonly IMapper mapper;

        public ImageController(ICloudinaryService cloudinaryService,
            IImageServices imageServices,
            IMapper mapper)
        {
            this.cloudinaryService = cloudinaryService;
            this.imageServices = imageServices;
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

            RedirectToActionResult redirectResult = new RedirectToActionResult("Create", "Offer", new { @Id = $"{id}" });
            return redirectResult;
        }

        [HttpGet("/Image/Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            this.ViewData["Id"] = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(List<ImageLoadViewModel> model)
        {
            return View();
        }

    }
}