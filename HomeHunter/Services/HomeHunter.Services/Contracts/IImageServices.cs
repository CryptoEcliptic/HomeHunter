using HomeHunter.Services.Models.Image;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IImageServices
    {
        Task<bool> AddImageAsync(string url, string estateId);

       ImageLoadServiceModel LoadImagesAsync(string offerId);
    }
}
