using AutoMapper;
using HomeHunter.Domain;
using HomeHunter.Domain.Enums;
using HomeHunter.Models.BindingModels.Offer;
using HomeHunter.Models.ViewModels.Offer;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.Offer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeHunter.App.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        private readonly IMapper mapper;
        private readonly IOfferServices offerServices;
        private readonly SignInManager<HomeHunterUser> signInManager;
        private readonly IVisitorSessionServices visitorSessionServices;
        private readonly IHttpContextAccessor accessor;

        public OfferController(
            IMapper mapper,
            IOfferServices offerServices,
            SignInManager<HomeHunterUser> signInManager,
            IVisitorSessionServices visitorSessionServices,
            IHttpContextAccessor accessor)
        {
            this.mapper = mapper;
            this.offerServices = offerServices;
            this.signInManager = signInManager;
            this.visitorSessionServices = visitorSessionServices;
            this.accessor = accessor;
        }


        // GET: Offer
        public async Task<IActionResult> Index()
        {
            var offerIndexServiceModel = await this.offerServices.GetAllActiveOffersAsync(null);
            var offers = this.mapper.Map<IEnumerable<OfferIndexViewModel>>(offerIndexServiceModel);

            this.ViewData["Deactivated"] = false;
            return View(offers);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IndexDeleted()
        {
            var offerIndexServiceModel = await this.offerServices.GetAllDeactivatedOffersAsync();

            var offerIndexViewModel = this.mapper.Map<IEnumerable<OfferIndexViewModel>>(offerIndexServiceModel);
            this.ViewData["Deactivated"] = true;

            return View("Index", offerIndexViewModel);
        }


        [AllowAnonymous]
        public async Task<IActionResult> IndexSales()
        {
            var ip = this.accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            string visitorId = HttpContext.Request.Cookies["VisitorId"];
            await this.visitorSessionServices.AddSessionInTheDb(ip, visitorId);

            var condition = OfferType.Sale;

            var offerIndexServiceModel = await this.offerServices.GetAllActiveOffersAsync(condition);
            var offersIndexGuestViewModel = this.mapper.Map<IEnumerable<OfferIndexGuestViewModel>>(offerIndexServiceModel).ToList();
            this.ViewData["Title"] = "Sales";
            return View("IndexOffers", offersIndexGuestViewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> IndexRentals()
        {
            var ip = this.accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            string visitorId = HttpContext.Request.Cookies["VisitorId"];
            await this.visitorSessionServices.AddSessionInTheDb(ip, visitorId);

            var condition = OfferType.Rental;

            var offerIndexServiceModel = await this.offerServices.GetAllActiveOffersAsync(condition);
            var offersIndexGuestViewModel = this.mapper.Map<IEnumerable<OfferIndexGuestViewModel>>(offerIndexServiceModel).ToList();

            return View("IndexOffers", offersIndexGuestViewModel);
        }

        //GET: Offer/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerDetailsServiceModel = await this.offerServices.GetOfferDetailsAsync(id);

            if (!this.signInManager.IsSignedIn(User))
            {
                var offerDetailsGuestViewModel = this.mapper.Map<OfferDetailsGuestViewModel>(offerDetailsServiceModel);
                return View("DetailsGuest", offerDetailsGuestViewModel);
            }

            var offerDetailViewModel = this.mapper.Map<OfferDetailsViewModel>(offerDetailsServiceModel);
            return View(offerDetailViewModel);
        }
        //GET: Offer/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DetailsDeleted(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offerDetailsServiceModel = await this.offerServices.GetOfferDetailsAsync(id);
            var offerDetailViewModel = this.mapper.Map<OfferDetailsDeletedViewModel>(offerDetailsServiceModel);

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

            var authorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
