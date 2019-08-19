using AutoMapper;
using HomeHunter.Domain;
using HomeHunter.Domain.Enums;
using HomeHunter.Models.ViewModels.Offer;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.EmailSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.App.Controllers
{
    public class HomeController : Controller
    {
        private const string SuccessfullySentQuestionMessage = "Успешно изпратихте запитване към служителите ни!";

        private readonly IUserServices usersService;
        private readonly UserManager<HomeHunterUser> userManager;
        private readonly IOfferServices offerServices;
        private readonly IMapper mapper;
        private readonly IApplicationEmailSender emailSender;

        public HomeController(IUserServices usersService,
            UserManager<HomeHunterUser> userManager,
            IOfferServices offerServices,
            IMapper mapper,
            IApplicationEmailSender emailSender)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.offerServices = offerServices;
            this.mapper = mapper;
            this.emailSender = emailSender;
        }
        public IActionResult Index()
        {
            if (this.User.IsInRole("User"))
            {
                return RedirectToAction("AuthenticatedIndex", "Home");
            }
            else if (this.User.IsInRole("Admin"))
            {
                return LocalRedirect("~/Administration");
            }

            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult AuthenticatedIndex()
        {
            var userId = this.userManager.GetUserId(this.User);
            var isUserEmailAuthenticated = this.usersService.IsUserEmailAuthenticated(userId)
                && this.User.Identity.IsAuthenticated;

            return View(isUserEmailAuthenticated);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Contact(OfferDetailsGuestViewModel returnModel)
        {
            var model = returnModel.ContactFormBindingModel;

            if (!this.ModelState.IsValid)
            {
                return new RedirectToActionResult("Home", "Details", model.OfferId);
            }

            await this.emailSender.SendContactFormEmailAsync(model.Email, model.Name + " " + model.ReferenceNumber, model.Message);

            this.TempData["SuccessfullSubmition"] = SuccessfullySentQuestionMessage;

            return Redirect($"/Home/Details/{model.OfferId}");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
