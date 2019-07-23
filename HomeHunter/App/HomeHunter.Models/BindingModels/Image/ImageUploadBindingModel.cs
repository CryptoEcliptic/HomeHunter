using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HomeHunter.Models.BindingModels.Image
{
    public class ImageUploadBindingModel
    {
        public List<IFormFile> Images { get; set; } 
    }
}
