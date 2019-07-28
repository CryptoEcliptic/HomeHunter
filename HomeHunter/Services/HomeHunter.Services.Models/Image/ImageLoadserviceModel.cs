using System.Collections.Generic;

namespace HomeHunter.Services.Models.Image
{
    public class ImageLoadServiceModel
    {
        public ImageLoadServiceModel()
        {
            this.DelitableImages = new List<DelitableImageServiceModel>();
        }
        public List<DelitableImageServiceModel> DelitableImages { get; set; }
    }
}
