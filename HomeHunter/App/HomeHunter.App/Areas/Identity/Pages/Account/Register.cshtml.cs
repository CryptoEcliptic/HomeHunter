using HomeHunter.Domain;
using HomeHunterCommon;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HomeHunter.App.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<HomeHunterUser> _signInManager;
        private readonly UserManager<HomeHunterUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<HomeHunterUser> userManager,
            SignInManager<HomeHunterUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email*")]
            [StringLength(40, ErrorMessage = "{0} не трябва да надвишава {1} символа.")]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Телефон")]
            [StringLength(16, ErrorMessage = "{0}ът не трябва да надвишава {1} символа.")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(16, ErrorMessage = "Полето \"Име\" трябва да бъде от поне {2} и да не надвишава {1} символа.", MinimumLength = 3)]
            [Display(Name = "Име*")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(16, ErrorMessage = "Фамилията не трябва да надвишава {1} символа.")]
            [Display(Name = "Фамилия*")]
            public string LastName { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "Паролатата трябва да бъде от поне {2} и да не надвишава {1} символа.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Парола*")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Потвърдете паролата*")]
            [Required]
            [Compare("Password", ErrorMessage = "Данните в полета \"Парола\" и \"Потвърдете паролата\" трябва да съвпадат.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new HomeHunterUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    CreatedOn = DateTime.UtcNow,
                    PhoneNumber = Input.PhoneNumber,
                    IsDeleted = false
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, $"Потвърждаване на регистрацията Ви в {GlobalConstants.CompanyName}",
                        $"Благодарим Ви, че се регистрирахте в интернет страницата на {GlobalConstants.CompanyName}! За да потвърдите валидността на email-a си, моля последвайте <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>линка</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
