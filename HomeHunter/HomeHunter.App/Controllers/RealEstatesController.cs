using AutoMapper;
using HomeHunter.App.Models.BuildingType;
using HomeHunter.App.Models.HeatingSystem;
using HomeHunter.App.Models.RealEstateType;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services;
using HomeHunter.Services.Contracts;
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
        private readonly HomeHunterDbContext _context;
        private readonly IRealEstateTypeService realEstateTypeService;
        private readonly IHeatingSystemServices heatyngSystemservices;
        private readonly IBuildingTypeServices buildingTypeServices;
        private readonly IMapper mapper;

        public RealEstatesController(HomeHunterDbContext context, 
            IRealEstateTypeService realEstateTypeService,
            IHeatingSystemServices heatyngSystemservices, 
            IBuildingTypeServices buildingTypeServices, IMapper mapper)
        {
            _context = context;
            this.realEstateTypeService = realEstateTypeService;
            this.heatyngSystemservices = heatyngSystemservices;
            this.buildingTypeServices = buildingTypeServices;
            this.mapper = mapper;
        }

        // GET: RealEstates
        public async Task<IActionResult> Index()
        {
            var homeHunterDbContext = _context.RealEstates.Include(r => r.BuildingType).Include(r => r.HeatingSystem).Include(r => r.RealEstateType);
            return View(await homeHunterDbContext.ToListAsync());
        }

        // GET: RealEstates/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstate = await _context.RealEstates
                .Include(r => r.BuildingType)
                .Include(r => r.HeatingSystem)
                .Include(r => r.RealEstateType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realEstate == null)
            {
                return NotFound();
            }

            return View(realEstate);
        }

        // GET: RealEstates/Create
        public IActionResult Create()
        {
            ViewData["BuildingTypeId"] = new SelectList(_context.BuildingTypes, "Id", "Name");
            ViewData["HeatingSystemId"] = new SelectList(_context.HeatingSystems, "Id", "Name");
            ViewData["RealEstateTypeId"] = new SelectList(_context.RealEstateTypes, "Id", "TypeName");

            var realEstateTypes = this.realEstateTypeService.GetAllTypes();
            var realEstateTypesVewModel = this.mapper.Map<IList<RealEstateTypeVewModel>>(realEstateTypes);

            var buildingTypes = this.buildingTypeServices.GetAllBuildingTypes();
            var buildingTypesVewModel = this.mapper.Map<IList<BuildingTypeVewModel>>(buildingTypes);

            var heatingSystems = this.heatyngSystemservices.GetAllHeatingSystems();
            var heatingSystemsVewModel = this.mapper.Map<IList<HeatingSystemViewModel>>(heatingSystems);

            return View();
        }

        // POST: RealEstates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FloorNumber,BuildingTotalFloors,Area,Price,Year,ParkingPlace,Yard,MetroNearBy,Balcony,CellingOrBasement,HeatingSystemId,RealEstateTypeId,BuildingTypeId,Id,CreatedOn,ModifiedOn,IsDeleted,DeletedOn")] RealEstate realEstate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(realEstate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingTypeId"] = new SelectList(_context.BuildingTypes, "Id", "Name", realEstate.BuildingTypeId);
            ViewData["HeatingSystemId"] = new SelectList(_context.HeatingSystems, "Id", "Name", realEstate.HeatingSystemId);
            ViewData["RealEstateTypeId"] = new SelectList(_context.RealEstateTypes, "Id", "TypeName", realEstate.RealEstateTypeId);
            return View(realEstate);
        }

        // GET: RealEstates/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstate = await _context.RealEstates.FindAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }
            ViewData["BuildingTypeId"] = new SelectList(_context.BuildingTypes, "Id", "Name", realEstate.BuildingTypeId);
            ViewData["HeatingSystemId"] = new SelectList(_context.HeatingSystems, "Id", "Name", realEstate.HeatingSystemId);
            ViewData["RealEstateTypeId"] = new SelectList(_context.RealEstateTypes, "Id", "TypeName", realEstate.RealEstateTypeId);
            return View(realEstate);
        }

        // POST: RealEstates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FloorNumber,BuildingTotalFloors,Area,Price,Year,ParkingPlace,Yard,MetroNearBy,Balcony,CellingOrBasement,HeatingSystemId,RealEstateTypeId,BuildingTypeId,Id,CreatedOn,ModifiedOn,IsDeleted,DeletedOn")] RealEstate realEstate)
        {
            if (id != realEstate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(realEstate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealEstateExists(realEstate.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingTypeId"] = new SelectList(_context.BuildingTypes, "Id", "Name", realEstate.BuildingTypeId);
            ViewData["HeatingSystemId"] = new SelectList(_context.HeatingSystems, "Id", "Name", realEstate.HeatingSystemId);
            ViewData["RealEstateTypeId"] = new SelectList(_context.RealEstateTypes, "Id", "TypeName", realEstate.RealEstateTypeId);
            return View(realEstate);
        }

        // GET: RealEstates/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstate = await _context.RealEstates
                .Include(r => r.BuildingType)
                .Include(r => r.HeatingSystem)
                .Include(r => r.RealEstateType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realEstate == null)
            {
                return NotFound();
            }

            return View(realEstate);
        }

        // POST: RealEstates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var realEstate = await _context.RealEstates.FindAsync(id);
            _context.RealEstates.Remove(realEstate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealEstateExists(string id)
        {
            return _context.RealEstates.Any(e => e.Id == id);
        }
    }
}
