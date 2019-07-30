using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HomeHunter.Services.Models.Image
{
    public class ImageUploadEditServiceModel
    {
        public ImageUploadEditServiceModel()
        {
            this.Images = new List<IFormFile>();
        }

        public int AlreadyUploadedImagesCount { get; set; }

        public string RealEstateId { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
