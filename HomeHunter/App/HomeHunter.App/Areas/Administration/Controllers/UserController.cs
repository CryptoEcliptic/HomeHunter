using AutoMapper;
using HomeHunter.Models.BindingModels.User;
using HomeHunter.Models.ViewModels.User;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.App.Areas.Administration.Controllers
{
    public class UserController : AdminController
    {
        private readonly IUserServices userServices;
        private readonly IMapper mapper;

        public UserController(IUserServices userServices, IMapper mapper)
        {
            this.userServices = userServices;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userIndexServiceModel = await this.userServices.GetAllUsersAsync();
            var userIndexViewModel = this.mapper.Map<List<UserIndexViewModel>>(userIndexServiceModel);

            return View(userIndexViewModel);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model ?? new UserCreateBindingModel());
            }

            var userCreateServiceModel = this.mapper.Map<UserCreateServiceModel>(model);
            var userData = await this.userServices.CreateUser(userCreateServiceModel);

            var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userData.UserId, code = userData.Code },
                        protocol: Request.Scheme);

            var isVerificationEmailSent = await this.userServices.SendVerificationEmail(callbackUrl, userData.Email);

            return RedirectToAction(nameof(Index));
        }

        //// GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDetailsServiceModel = await this.userServices.GetUserDetailsAsync(id);

            var userDetailsViewModel = this.mapper.Map<UserDetailsViewModel>(userDetailsServiceModel);

            return View(userDetailsViewModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isUserDeleted = await this.userServices.SoftDeleteUserAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}