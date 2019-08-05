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

            if (userData == null)
            {
                //TODO Handle Error
            }

            var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userData.UserId, code = userData.Code },
                        protocol: Request.Scheme);

            var isVerificationEmailSent = await this.userServices.SendVerificationEmail(callbackUrl, userData.Email);

            if (isVerificationEmailSent)
            {
                //TODO Handle Error
            }

            return RedirectToAction(nameof(Index));
        }

        //// GET: Offer/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var userDetailsServiceModel = await this.userServices.GetUserDetailsAsync(id);

            if (userDetailsServiceModel == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var userDetailsViewModel = this.mapper.Map<UserDetailsViewModel>(userDetailsServiceModel);

            return View(userDetailsViewModel);
        }

        // POST: Offer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
           var isUserDeleted = await this.userServices.SoftDeleteUserAsync(id);

            if (!isUserDeleted)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction(nameof(Index));
        }


    }
}