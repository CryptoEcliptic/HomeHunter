using Microsoft.AspNetCore.Mvc;

namespace HomeHunter.App.Areas.Administration.Controllers
{
    public class HomeController : AdminController
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}