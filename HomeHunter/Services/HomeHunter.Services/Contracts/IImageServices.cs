using HomeHunter.Services.Models.Image;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IImageServices
    {
        Task<bool> AddImageAsync(string publikKey, string url, string estateId);

        ImageLoadServiceModel LoadImagesAsync(string offerId);

        Task<ImageUploadEditServiceModel> GetImageDetailsAsync(string offerId);

        int ImagesCount(string id);

        Task<bool> EditImageAsync(string publicKey, string url, string estateId);

        Task<int> RemoveImages(string estateId);

        Task <IEnumerable<string>> GetImageIds(string realEstateId);
    }
}
