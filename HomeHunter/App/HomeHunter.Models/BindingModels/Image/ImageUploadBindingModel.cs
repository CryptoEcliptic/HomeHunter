using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHunter.Models.BindingModels.Image
{
    public class ImageUploadBindingModel
    {
        public ImageUploadBindingModel()
        {
            this.Images = new List<IFormFile>();
        }

        [Url]
        public List<IFormFile> Images { get; set; } 
    }
}
