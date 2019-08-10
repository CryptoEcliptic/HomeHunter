using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HomeHunter.App.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
