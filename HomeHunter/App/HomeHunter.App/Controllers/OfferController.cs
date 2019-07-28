using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Models.BindingModels.Offer;
using HomeHunter.Models.ViewModels.Offer;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.Offer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeHunter.App.Controllers
{
    public class OfferController : Controller
    {
        private readonly HomeHunterDbContext _context;
        private readonly IMapper mapper;
        private readonly IOfferServices offerServices;

        public OfferController(HomeHunterDbContext context, 
            IMapper mapper,
            IOfferServices offerServices)
        {
            _context = context;
            this.mapper = mapper;
            this.offerServices = offerServices;
        }

        // GET: Offer
        public async Task<IActionResult> Index()
        {
            var offerIndexServiceModel = await this.offerServices.GetAllActiveOffersAsync();
            var offers = this.mapper.Map<IEnumerable<OfferIndexViewModel>>(offerIndexServiceModel);

            return View(offers);
        }

        //GET: Offer/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await this.offerServices.GetOfferDetailsAsync(id);
           
            if (offer == null)
            {
                return NotFound();
            }

            var offerDetailViewModel = this.mapper.Map<OfferDetailsViewModel>(offer);
            //map

            return View(offerDetailViewModel);
        }

        // GET: Offer/Create
        [HttpGet("/Offer/Create/{estateId}")]
        public IActionResult Create(string estateId)
        {
           //TODo OfferCreateBindingModel
            if (estateId == null)
            {
                return NotFound();
            }
            
            return View();
        }

        // POST: Offer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id, OfferCreateBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model ?? new OfferCreateBindingModel());
            }

            var authorId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var mappedOffer = this.mapper.Map<OfferCreateServiceModel>(model);
            var isOfferCreated = await this.offerServices.CreateOfferAsync(authorId, id, mappedOffer);

            if (!isOfferCreated)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Offer/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await this.offerServices.GetOfferByIdAsync(id);

            if (offer == null)
            {
                return NotFound();
            }

            var offerEditViewModel = this.mapper.Map<OfferEditBindingModel>(offer);
            

            return View(offerEditViewModel);
        }

        // POST: Offer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, OfferEditBindingModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var offer = this.mapper.Map<OfferEditServiceModel>(model);
            var isOfferEdited = await this.offerServices.EditOfferAsync(offer);

            if (!isOfferEdited)
            {
                return RedirectToAction("Error", "Home");
            }

            RedirectToActionResult redirectResult = new RedirectToActionResult("Edit", "Image", new { @Id = $"{id}" });
            return redirectResult;

        }

        //// GET: Offer/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var offer = await _context.Offers
        //        .Include(o => o.Author)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (offer == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(offer);
        //}

        // POST: Offer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //private bool OfferExists(int id)
        //{
        //    return _context.Offers.Any(e => e.Id == id);
        //}
    }
}
