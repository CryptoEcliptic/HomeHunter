using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HomeHunter.Models.ViewModels.Image
{
    public class ImageEditViewModel
    {
        public List<IFormFile> Images { get; set; }
    }
}
