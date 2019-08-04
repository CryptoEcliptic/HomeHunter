using AutoMapper;
using HomeHunter.Models.ViewModels.User;
using HomeHunter.Services.Contracts;
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
    }
}