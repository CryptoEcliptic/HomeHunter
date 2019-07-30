using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        [Url]
        public List<IFormFile> Images { get; set; }
    }
}
