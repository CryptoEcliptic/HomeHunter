using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HomeHunter.Infrastructure.CloudinaryServices
{
    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string name);
    }
}
