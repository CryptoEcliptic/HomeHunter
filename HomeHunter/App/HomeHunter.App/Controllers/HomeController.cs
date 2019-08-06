using HomeHunter.Models;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace HomeHunter.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserServices usersService;
        private readonly UserManager<HomeHunterUser> userManager;

        public HomeController(IUserServices usersService, UserManager<HomeHunterUser> userManager)
        {
            this.usersService = usersService;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public IActionResult AuthenticatedIndex()
        {
            var userId = this.userManager.GetUserId(this.User);
            var isUseremailAuthenticated = this.usersService.IsUserEmailAuthenticated(userId)
                && this.User.Identity.IsAuthenticated;

            return View(isUseremailAuthenticated);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
