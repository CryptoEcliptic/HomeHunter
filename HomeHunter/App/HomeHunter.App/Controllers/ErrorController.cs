using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HomeHunter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HomeHunter.App.Controllers
{
    public class ErrorController : Controller
    {
        private readonly string Error404Message = "Error 404: Страницата не може да бъде намерена!";
        private readonly string GeneralErrorMessage= "Възникна грешка! Работим за отстраняване на проблема. Моля, опитайте по-късно!";

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
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

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            this.ViewData["GenErrorMessage"] = GeneralErrorMessage;
            

            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}