using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using HomeHunter.Domain;
using HomeHunter.Common;

namespace HomeHunter.App.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private const string NotLoadUserErrorMessage = "Unable to load user for update last login.";
        private const string AccountLockedOutMessage = "User account locked out.";
        private const string InvalidUserDataErrorMessage = "Невалидни потребителски данни.";
        private const string UserLoggedInLogMessage = "User logged in.";
        private readonly SignInManager<HomeHunterUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<HomeHunterUser> userManager;

        public LoginModel(SignInManager<HomeHunterUser> signInManager, ILogger<LoginModel> logger, UserManager<HomeHunterUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Парола")]
            public string Password { get; set; }

            [Display(Name = "Запомни паролата?")]
            public bool RememberMe { get; set; }
        }

        public async Task<ActionResult> OnGetAsync(string returnUrl = null)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await this.userManager.FindByNameAsync(Input.Email);
                    if (user == null)
                    {
                        return NotFound(NotLoadUserErrorMessage);
                    }
                    user.LastLogin = DateTime.UtcNow;
                    var lastLoginResult = await this.userManager.UpdateAsync(user);
                    var roles = await userManager.GetRolesAsync(user);

                    _logger.LogInformation(UserLoggedInLogMessage);

                    if (roles.Contains(GlobalConstants.AdministratorRoleName))
                    {
                        returnUrl = "~/Administration/Home/Index";
                    }
                    else if (roles.Contains(GlobalConstants.UserRoleName))
                    {
                        returnUrl = "~/Home/AuthenticatedIndex";
                    }
                   
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(AccountLockedOutMessage);
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, InvalidUserDataErrorMessage);
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
