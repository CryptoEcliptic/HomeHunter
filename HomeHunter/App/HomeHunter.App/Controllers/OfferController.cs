using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Infrastructure;
using HomeHunter.Models.BindingModels.Offer;
using HomeHunter.Models.ViewModels.Offer;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.Offer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeHunter.App.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        private const int DefaultPageSize = 8;
        private readonly IMapper mapper;
        private readonly IOfferServices offerServices;

        public OfferController(
            IMapper mapper,
            IOfferServices offerServices)
        {
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

        [AllowAnonymous]
        public async Task<IActionResult> IndexSales(int? pageNumber)
        {
            var offerIndexServiceModel = await this.offerServices.GetAllSalesOffersAsync();
            var offers = this.mapper.Map<IEnumerable<OfferIndexSalesViewModel>>(offerIndexServiceModel).ToList();

            return View(PaginationList<OfferIndexSalesViewModel>.Create(offers, pageNumber ?? 1, DefaultPageSize));
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IndexDeactivated()
        {
            var offerIndexServiceModel = await this.offerServices.GetAllDeactivatedOffersAsync();

            var offers = this.mapper.Map<IEnumerable<OfferIndexDeactivatedViewModel>>(offerIndexServiceModel);

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
            var offerDetailViewModel = this.mapper.Map<OfferDetailsViewModel>(offer);

            return View(offerDetailViewModel);
        }
        //GET: Offer/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DetailsDeactivated(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await this.offerServices.GetOfferDetailsAsync(id);
            var offerDetailViewModel = this.mapper.Map<OfferDetailsDeactivatedViewModel>(offer);

            return View(offerDetailViewModel);
        }

        // GET: Offer/Create
        [HttpGet("/Offer/Create/{estateId}")]
        public IActionResult Create(string estateId)
        {
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

            if (authorId == null)
            {
                return NotFound();
            }

            var mappedOffer = this.mapper.Map<OfferCreateServiceModel>(model);

            var isOfferCreated = await this.offerServices.CreateOfferAsync(authorId, id, mappedOffer);

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

            RedirectToActionResult redirectResult = new RedirectToActionResult("Edit", "Image", new { @Id = $"{id}" });
            return redirectResult;

        }

        //// GET: Offer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerDetailsServiceModel = await this.offerServices.GetOfferDetailsAsync(id);

            var offerDetailViewModel = this.mapper.Map<OfferDetailsViewModel>(offerDetailsServiceModel);

            return View(offerDetailViewModel);
        }

        // POST: Offer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var isOfferDeleted = await this.offerServices.DeleteOfferAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
