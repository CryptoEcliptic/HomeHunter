using AutoMapper;
using HomeHunter.Common;
using HomeHunter.Domain;
using HomeHunter.Domain.Enums;
using HomeHunter.Models.BindingModels.Home;
using HomeHunter.Models.MLModels;
using HomeHunter.Models.ViewModels.Offer;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.EmailSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
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
        private readonly IApplicationEmailSender emailSender;
        private readonly IVisitorSessionServices visitorSessionServices;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> predictionEngine;
        private readonly IHttpContextAccessor accessor;
        private readonly IMapper mapper;

        public HomeController(IUserServices usersService,
            UserManager<HomeHunterUser> userManager,
            IApplicationEmailSender emailSender,
            IVisitorSessionServices visitorSessionServices,
            PredictionEnginePool<ModelInput, ModelOutput> predictionEngine,
            IHttpContextAccessor accessor,
            IMapper mapper)
        {
            this.usersService = usersService;
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.visitorSessionServices = visitorSessionServices;
            this.predictionEngine = predictionEngine;
            this.accessor = accessor;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var ip = this.accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            string visitorId = HttpContext.Request.Cookies["VisitorId"];
            
            this.visitorSessionServices.AddSessionInTheDb(ip, visitorId);

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

        //[AllowAnonymous]
        [HttpGet]
        public IActionResult PredictPrice()
        {
            LoadDropdownMenusData();
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AllowAnonymous]
        public IActionResult PredictPrice(PricePredictionBindingModel model)
        {
            var input = this.mapper.Map<ModelInput>(model);
            var output = this.predictionEngine.Predict(input);

            LoadDropdownMenusData();
            model.Price = output.Score;

            return this.View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Contact(OfferDetailsGuestViewModel returnModel)
        {
            var model = returnModel.ContactFormBindingModel;

            if (!this.ModelState.IsValid)
            {
                return new RedirectToActionResult("Offer", "Details", model.OfferId);
            }

            await this.emailSender.SendContactFormEmailAsync(model.Email, model.Name + " " + model.ReferenceNumber, model.Message);

            this.TempData["SuccessfullSubmition"] = SuccessfullySentQuestionMessage;

            return Redirect($"/Offer/Details/{model.OfferId}");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [NonAction]
        private void LoadDropdownMenusData()
        {
            this.ViewData["AppartmentTypes"] = GlobalConstants.ImotBgAppartmentTypes;
            this.ViewData["Districts"] = GlobalConstants.ImotBgSofiaDistricts;
            this.ViewData["BuildingTypes"] = GlobalConstants.ImotBgBuildingTypes;
        }
    }
}
