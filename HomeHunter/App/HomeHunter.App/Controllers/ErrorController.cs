using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HomeHunter.App.Controllers
{
    public class ErrorController : Controller
    {
        private readonly string Error404Message = "Error 404: Страницата не може да бъде намерена!";

        [Route("Error/{statusCode}")]
        public async Task<IActionResult> HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    this.ViewData["Error"] = Error404Message;
                    break;

                default:
                    break;
            }
            return View("NotFound");
        }
    }
}