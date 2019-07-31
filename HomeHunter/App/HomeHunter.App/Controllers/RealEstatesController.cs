using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Models.BindingModels.RealEstate;
using HomeHunter.Models.ViewModels.BuildingType;
using HomeHunter.Models.ViewModels.City;
using HomeHunter.Models.ViewModels.HeatingSystem;
using HomeHunter.Models.ViewModels.Neighbourhood;
using HomeHunter.Models.ViewModels.RealEstate;
using HomeHunter.Models.ViewModels.RealEstateType;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.RealEstate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.App.Controllers
{
    [Authorize]
    public class RealEstatesController : Controller
    {
        private const string FloorNumbersError = "Етажът не може да бъде по-голям от общия брой етажи!";

        private readonly IRealEstateTypeServices realEstateTypeService;
        private readonly IHeatingSystemServices heatingSystemservices;
        private readonly IBuildingTypeServices buildingTypeServices;
        private readonly ICitiesServices citiesServices;
        private readonly IRealEstateServices realEstateServices;
        private readonly INeighbourhoodServices neighbourhoodServices;
        private readonly IOfferServices offerServices;
        private readonly IMapper mapper;

        public RealEstatesController(
            IRealEstateTypeServices realEstateTypeService,
            IHeatingSystemServices heatingSystemservices, 
            IBuildingTypeServices buildingTypeServices, 
            ICitiesServices citiesServices,
            IRealEstateServices realEstateServices,
            INeighbourhoodServices neighbourhoodServices,
            IOfferServices offerServices,
            IMapper mapper)
        {
           
            this.realEstateTypeService = realEstateTypeService;
            this.heatingSystemservices = heatingSystemservices;
            this.buildingTypeServices = buildingTypeServices;
            this.citiesServices = citiesServices;
            this.realEstateServices = realEstateServices;
            this.neighbourhoodServices = neighbourhoodServices;
            this.offerServices = offerServices;
            this.mapper = mapper;
        }

        // GET: RealEstates
        public async Task<IActionResult> Index()
        {
            var realEstates = await this.realEstateServices.GetAllRealEstatesAsync();
            var realEstatesViewModel = this.mapper.Map<IEnumerable<RealEstateIndexViewModel>>(realEstates);

            return View(realEstatesViewModel);
           
        }

        //GET: RealEstates/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstateServiceModel = await this.realEstateServices.GetDetailsAsync(id);

            if (realEstateServiceModel == null)
            {
                return NotFound();
            }

            var reaiEstateDetailsViewModel = this.mapper.Map<RealEstateDetailsViewModel>(realEstateServiceModel);

            return View(reaiEstateDetailsViewModel);
        }

        // GET: RealEstates/Create
        public async Task<IActionResult> Create()
        {
          
            await this.LoadDropdownMenusData();
            return View(new CreateRealEstateBindingModel());
        }

        // POST: RealEstates/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRealEstateBindingModel model)
        {
            if (int.TryParse(model.FloorNumber, out int floor))
            {
                if (floor > model.BuildingTotalFloors)
                {
                    ModelState.AddModelError("TotalFloors", FloorNumbersError);
                }
            }

            if (ModelState.IsValid)
            {
                var realEstate = this.mapper.Map<RealEstateCreateServiceModel>(model);

                var realEstateId = await this.realEstateServices.CreateRealEstateAsync(realEstate);

                if (realEstateId == null)
                {
                    return RedirectToAction("Error", "Home");
                }

                RedirectToActionResult redirectResult = new RedirectToActionResult("Upload", "Image", new { @Id = $"{realEstateId}" });
                return redirectResult;
            }

            await this.LoadDropdownMenusData();
            return View(model ?? new CreateRealEstateBindingModel());
        }

        // GET: RealEstates/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstate = await this.realEstateServices.GetDetailsAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var realEstateEditModel = this.mapper.Map<RealEstateEditBindingModel>(realEstate);

            await this.LoadDropdownMenusData();

            return View(realEstateEditModel);
        }

        //// POST: RealEstates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RealEstateEditBindingModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (int.TryParse(model.FloorNumber, out int floor))
            {
                if (floor > model.BuildingTotalFloors)
                {
                    ModelState.AddModelError("TotalFloors", FloorNumbersError);
                }
            }

            if (ModelState.IsValid)
            {
                var realEstateToEdit = this.mapper.Map<RealEstateEditServiceModel>(model);

                var isRealEstateEddited = await this.realEstateServices.EditRealEstateAsync(realEstateToEdit);

                if (!isRealEstateEddited)
                {
                    return RedirectToAction("Error", "Home");
                }

                var offerId = await this.offerServices.GetOfferIdByRealEstateIdAsync(id);
                RedirectToActionResult redirectResult = new RedirectToActionResult("Details", "Offer", new { @Id = $"{offerId}"});
                return redirectResult;

            }
            await this.LoadDropdownMenusData();
            return View(model);
        }

        //// GET: RealEstates/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstate = await this.realEstateServices.GetDetailsAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            var realEstateEditModel = this.mapper.Map<RealEstateDetailsViewModel>(realEstate);
            return View(realEstateEditModel);
        }

        //// POST: RealEstates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isRealEstateDeleted = await this.realEstateServices.DeleteRealEstateAsync(id);

            if (!isRealEstateDeleted)
            {
                return NotFound();
            }
           
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<JsonResult> GetNeighbourhoodsList(string cityName)
        {
            var neighbourhoods = await this.neighbourhoodServices.GetNeighbourhoodsByCityAsync(cityName);
            var neighbourhoodsVewModel = this.mapper.Map<IList<NeighbourhoodViewModel>>(neighbourhoods);

            var neighbourhoodlist = new SelectList(neighbourhoodsVewModel.Select(x => x.Name));
            return Json(neighbourhoodlist);

        }

        [NonAction]
        private async Task LoadDropdownMenusData()
        {
            var realEstateTypes = await this.realEstateTypeService.GetAllTypesAsync();
            var realEstateTypesVewModel = this.mapper.Map<List<RealEstateTypeViewModel>>(realEstateTypes);

            var buildingTypes = await Task.Run(() => this.buildingTypeServices.GetAllBuildingTypesAsync());
            var buildingTypesVewModel = this.mapper.Map<List<BuildingTypeViewModel>>(buildingTypes);

            var heatingSystems = await this.heatingSystemservices.GetAllHeatingSystemsAsync();
            var heatingSystemsVewModel = this.mapper.Map<List<HeatingSystemViewModel>>(heatingSystems);

            var cities = await this.citiesServices.GetAllCitiesAsync();
            var citiesVewModel = this.mapper.Map<List<CityViewModel>>(cities);


            this.ViewData["RealEstateTypes"] = realEstateTypesVewModel;
            this.ViewData["HeatingSystems"] = heatingSystemsVewModel;
            this.ViewData["Cities"] = citiesVewModel;
            this.ViewData["BuildingTypes"] = buildingTypesVewModel;
           
        }
        
    }
}
