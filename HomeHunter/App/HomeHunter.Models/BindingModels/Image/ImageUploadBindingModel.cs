using HomeHunter.Common;
using HomeHunter.Infrastructure.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HomeHunter.Models.BindingModels.Image
{
    public class ImageUploadBindingModel
    {
        public ImageUploadBindingModel()
        {
            this.Images = new List<IFormFile>();
        }

        [AllowedFileExtensionAttribute(GlobalConstants.AllowedFileExtensionsAsString)]
        public List<IFormFile> Images { get; set; }

        public bool IsIndexImage { get; set; }

    }
}
