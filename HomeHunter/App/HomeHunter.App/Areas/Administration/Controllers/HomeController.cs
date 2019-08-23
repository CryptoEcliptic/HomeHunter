using HomeHunter.Models.ViewModels.Statistics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeHunter.App.Areas.Administration.Controllers
{
    public class HomeController : AdminController
    {

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Statistics()
        {

            return View();
        }
    }
}