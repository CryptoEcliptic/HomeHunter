using HomeHunter.Common;
using HomeHunter.Models.CustomValidationAttributse;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.Image
{
    public class ImageUploadEditBindingModel
    {
        public ImageUploadEditBindingModel()
        {
            this.Images = new List<IFormFile>();
        }

        public int AlreadyUploadedImagesCount { get; set; }

        public string RealEstateId { get; set; }


        [CustomFileExtensions(GlobalConstants.AllowedFileExtensionsAsString)]
        public List<IFormFile> Images { get; set; }


        public ImageUploadBindingModel ImageUploadBindingModel { get; set; }

    }
}
