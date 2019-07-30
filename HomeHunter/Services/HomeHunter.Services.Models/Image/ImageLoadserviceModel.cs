using System.Collections.Generic;

namespace HomeHunter.Services.Models.Image
{
    public class ImageLoadServiceModel
    {
        public ImageLoadServiceModel()
        {
            this.Images = new List<ImageChangeableServiceModel>();
        }
        public List<ImageChangeableServiceModel> Images { get; set; }
    }
}
