using AutoMapper;
using HomeHunter.Common;
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
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.App.Controllers
{
    [Authorize]
    public class RealEstatesController : Controller
    {
        private readonly IRealEstateTypeServices realEstateTypeService;
        private readonly IHeatingSystemServices heatingSystemservices;
        private readonly IBuildingTypeServices buildingTypeServices;
        private readonly ICitiesServices citiesServices;
        private readonly IRealEstateServices realEstateServices;
        private readonly INeighbourhoodServices neighbourhoodServices;
        private readonly IMapper mapper;
        private readonly HomeHunterDbContext _context;

        public RealEstatesController(
            IRealEstateTypeServices realEstateTypeService,
            IHeatingSystemServices heatingSystemservices, 
            IBuildingTypeServices buildingTypeServices, 
            ICitiesServices citiesServices,
            IRealEstateServices realEstateServices,
            INeighbourhoodServices neighbourhoodServices
            ,IMapper mapper)
        {
           
            this.realEstateTypeService = realEstateTypeService;
            this.heatingSystemservices = heatingSystemservices;
            this.buildingTypeServices = buildingTypeServices;
            this.citiesServices = citiesServices;
            this.realEstateServices = realEstateServices;
            this.neighbourhoodServices = neighbourhoodServices;
            this.mapper = mapper;
        }

        // GET: RealEstates
        public async Task<IActionResult> Index()
        {
            var realEstates = await this.realEstateServices.GetAllRealEstatesAsync();
            var mappedRealEstates = this.mapper.Map<IEnumerable<RealEstateIndexViewModel>>(realEstates);

            return View(mappedRealEstates);
           
        }

        //GET: RealEstates/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var realEstate = await _context.RealEstates
        //        .Include(r => r.BuildingType)
        //        .Include(r => r.HeatingSystem)
        //        .Include(r => r.RealEstateType)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (realEstate == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(realEstate);
        //}

        // GET: RealEstates/Create
        public async Task<IActionResult> Create()
        {
            
            await this.LoadDropdownMenusData();

            return View();
        }

        // POST: RealEstates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRealEstateBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var realEstate = this.mapper.Map<RealEstateCreateServiceModel>(model);

                var isRealEstateCreated = await this.realEstateServices.CreateRealEstateAsync(realEstate);

                return RedirectToAction(nameof(Index));
            }

            await this.LoadDropdownMenusData();
            await GetNeighbourhoodsList(model.City);

            return View(model ?? new CreateRealEstateBindingModel());
        }

        // GET: RealEstates/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var realEstate = await _context.RealEstates.FindAsync(id);
        //    if (realEstate == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["BuildingTypeId"] = new SelectList(_context.BuildingTypes, "Id", "Name", realEstate.BuildingTypeId);
        //    ViewData["HeatingSystemId"] = new SelectList(_context.HeatingSystems, "Id", "Name", realEstate.HeatingSystemId);
        //    ViewData["RealEstateTypeId"] = new SelectList(_context.RealEstateTypes, "Id", "TypeName", realEstate.RealEstateTypeId);
        //    return View(realEstate);
        //}

        //// POST: RealEstates/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("FloorNumber,BuildingTotalFloors,Area,Price,Year,ParkingPlace,Yard,MetroNearBy,Balcony,CellingOrBasement,HeatingSystemId,RealEstateTypeId,BuildingTypeId,Id,CreatedOn,ModifiedOn,IsDeleted,DeletedOn")] RealEstate realEstate)
        //{
        //    if (id != realEstate.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(realEstate);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RealEstateExists(realEstate.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["BuildingTypeId"] = new SelectList(_context.BuildingTypes, "Id", "Name", realEstate.BuildingTypeId);
        //    ViewData["HeatingSystemId"] = new SelectList(_context.HeatingSystems, "Id", "Name", realEstate.HeatingSystemId);
        //    ViewData["RealEstateTypeId"] = new SelectList(_context.RealEstateTypes, "Id", "TypeName", realEstate.RealEstateTypeId);
        //    return View(realEstate);
        //}

        //// GET: RealEstates/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var realEstate = await _context.RealEstates
        //        .Include(r => r.BuildingType)
        //        .Include(r => r.HeatingSystem)
        //        .Include(r => r.RealEstateType)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (realEstate == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(realEstate);
        //}

        //// POST: RealEstates/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var realEstate = await _context.RealEstates.FindAsync(id);
        //    _context.RealEstates.Remove(realEstate);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool RealEstateExists(string id)
        //{
        //    return _context.RealEstates.Any(e => e.Id == id);
        //}

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
