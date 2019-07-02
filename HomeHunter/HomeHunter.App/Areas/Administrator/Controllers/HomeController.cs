using Microsoft.AspNetCore.Mvc;

namespace HomeHunter.App.Areas.Administrator.Controllers
{
    public class HomeController : AdministratorController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}