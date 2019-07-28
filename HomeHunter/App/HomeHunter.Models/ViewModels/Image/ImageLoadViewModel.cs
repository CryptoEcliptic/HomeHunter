using HomeHunter.Services.Models.Image;
using System.Collections.Generic;

namespace HomeHunter.Models.ViewModels.Image
{
    public class ImageLoadViewModel
    {
        public ImageLoadViewModel()
        {
            this.DelitableImages = new List<DelitableImageServiceModel>();
        }

        public List<DelitableImageServiceModel> DelitableImages { get; set; }

    }
}
