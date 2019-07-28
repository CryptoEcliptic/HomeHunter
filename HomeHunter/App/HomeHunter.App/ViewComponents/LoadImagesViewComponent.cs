using AutoMapper;
using HomeHunter.Models.ViewModels.Image;
using HomeHunter.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeHunter.App.ViewComponents
{
    public class LoadImagesViewComponent : ViewComponent
    {
        private readonly IImageServices imageServices;
        private readonly IMapper mapper;

        public LoadImagesViewComponent(IImageServices imageServices, IMapper mapper)
        {
            this.imageServices = imageServices;
            this.mapper = mapper;
        }

        public IViewComponentResult Invoke(string id)
        {
            var imageLoadServiceModel = this.imageServices.LoadImagesAsync(id);

            var imageLoadVewModel = this.mapper.Map<ImageLoadViewModel>(imageLoadServiceModel);

            return View(imageLoadVewModel);
        }
    }
}
