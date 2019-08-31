using AutoMapper;
using HomeHunter.Common;
using HomeHunter.Data;
using HomeHunter.Domain;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.EmailSender;
using HomeHunter.Services.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class UserServices : PageModel, IUserServices
    {
        private const string UnsuccessfullyCreatedUserMessage = "User was not created!";
        private const string DeleteAdminDenialMessage = "Cannot delete Admin user!";
        private const string InvalidUserIdMessage = "User id is not valid";
        private const string UserNotFoundMessage = "No user with such Id in the database!";

        private readonly HomeHunterDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<HomeHunterUser> userManager;
        private readonly IApplicationEmailSender emailSender;

        public UserServices(HomeHunterDbContext context, 
            IMapper mapper,
            UserManager<HomeHunterUser> userManager,
            IApplicationEmailSender emailSender)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public async Task<IEnumerable<UserIndexServiceModel>> GetAllUsersAsync()
        {
            var usersFromDb = await this.userManager.Users
                .Where(x => x.IsDeleted == false)
                .ToListAsync();
                
            var userIndexServiceModel = this.mapper.Map<List<UserIndexServiceModel>>(usersFromDb);

            return userIndexServiceModel;
        }

        public async Task<UserReturnCreateServiceModel> CreateUser(UserCreateServiceModel model)
        {
            //Create user
            var user = new HomeHunterUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedOn = DateTime.UtcNow,
                PhoneNumber = model.PhoneNumber,
                IsDeleted = false,
                
            };

            //Save user in the db via UserManager
            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //Adds User role
                await userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);

                var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);

                var userReturnModel = new UserReturnCreateServiceModel
                {
                    UserId = user.Id,
                    Code = code,
                    Email = user.Email

                };
                return userReturnModel;
            }
            else
            {
                throw new InvalidOperationException(UnsuccessfullyCreatedUserMessage);
            }
        }

        public async Task<UserDetailsServiceModel> GetUserDetailsAsync(string userId)
        {
            var user = await this.GetUserById(userId);
            
            var userDetailsServiceModel = this.mapper.Map<UserDetailsServiceModel>(user);

            return userDetailsServiceModel;
        }

        public async Task<bool> SoftDeleteUserAsync(string userId)
        {
            var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            var roles = await this.userManager.GetRolesAsync(user);

            if (roles.Contains(GlobalConstants.AdministratorRoleName))
            {
                throw new InvalidOperationException(DeleteAdminDenialMessage);
            }

            user.IsDeleted = true;
            user.Email = null;
            user.FirstName = null;
            user.LastName = null;
            user.PhoneNumber = null;
            user.NormalizedEmail = null;
            user.NormalizedUserName = null;
            user.UserName = null;
            user.PasswordHash = null;
            user.EmailConfirmed = false;
            user.DeletedOn = DateTime.UtcNow;

            user.ModifiedOn = DateTime.UtcNow;

            try
            {
                this.context.Update(user);
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }

        public bool IsUserEmailAuthenticated(string userId)
        {
            var userFromDb = this.userManager.Users.FirstOrDefault(x => x.Id == userId);
            if (userFromDb != null)
            {
                if (userFromDb.EmailConfirmed == true)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> SendVerificationEmail(string callBackUrl, string email)
        {
            try
            {
                await this.emailSender.SendEmailAsync(email, $"Потвърждаване на регистрацията Ви в {GlobalConstants.CompanyName}",
               $"Благодарим Ви, че се регистрирахте в интернет страницата на {GlobalConstants.CompanyName}! За да потвърдите валидността на email-a си, моля последвайте <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>линка</a>.");
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }

        public async Task<HomeHunterUser> GetUserById(string userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(InvalidUserIdMessage);
            }
            else
            {
                var user = await this.userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

                if (user == null)
                {
                    throw new ArgumentNullException(UserNotFoundMessage);
                }
                else
                {
                    return user;
                }
            }
        }
    }
}
