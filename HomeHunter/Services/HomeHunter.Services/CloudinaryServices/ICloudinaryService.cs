using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Services.CloudinaryServices
{
    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string name);

        int DeleteCloudinaryImages(IEnumerable<string> imageIds);
    }
}
