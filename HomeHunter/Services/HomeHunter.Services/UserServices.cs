using AutoMapper;
using HomeHunter.Common;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeHunter.Services
{
    public class UserServices : PageModel, IUserServices
    {
        private readonly HomeHunterDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<HomeHunterUser> userManager;
        private readonly SignInManager<HomeHunterUser> signInManager;
        private readonly ILogger<UserServices> logger;
        private readonly IEmailSender emailSender;

        public UserServices(HomeHunterDbContext context, 
            IMapper mapper,
            UserManager<HomeHunterUser> userManager,
            SignInManager<HomeHunterUser> signInManager,
            ILogger<UserServices> logger,
            IEmailSender emailSender)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
        }

        public async Task<IEnumerable<UserIndexServiceModel>> GetAllUsersAsync()
        {
            var usersFromDb = await this.context.HomeHunterUsers.ToListAsync();

            var userIndexServiceModel = this.mapper.Map<IEnumerable<UserIndexServiceModel>>(usersFromDb);

            return userIndexServiceModel;
        }

        public bool IsUserEmailAuthenticated(string userId)
        {
            var userFromDb = this.context.HomeHunterUsers.FirstOrDefault(x => x.Id == userId);
            if (userFromDb != null)
            {
                if (userFromDb.EmailConfirmed == true)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<UserReturnCreateServiceModel> CreateUser(UserCreateServiceModel model)
        {
            var user = new HomeHunterUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedOn = DateTime.UtcNow,
                PhoneNumber = model.PhoneNumber,
                IsDeleted = false
            };

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                this.logger.LogInformation("User created a new account with password.");
                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                var userReturnModel = new UserReturnCreateServiceModel
                {
                    UserId = user.Id,
                    Code = code,
                    Email = user.Email

                };
                return userReturnModel;
            }

            return null;
        }

        public async Task<bool> SendVerificationEmail(string callBackUrl, string email)
        {
            try
            {
                await this.emailSender.SendEmailAsync(email, $"Потвърждаване на регистрацията Ви в {GlobalConstants.CompanyName}",
               $"Благодарим Ви, че се регистрирахте в интернет страницата на {GlobalConstants.CompanyName}! За да потвърдите валидността на email-a си, моля последвайте <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>линка</a>.");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

    }
}
