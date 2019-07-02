using HomeHunter.Domain;
using HomeHunterCommon;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Data.DataSeeding
{
    public class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(HomeHunterDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var usermanager = serviceProvider.GetRequiredService<UserManager<HomeHunterUser>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName); 
            await SeedRoleAsync(roleManager, GlobalConstants.UserRoleName);
            await SeedUserAdminRole(usermanager);
        }

        private static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedUserAdminRole(UserManager<HomeHunterUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new HomeHunterUser
                {
                    UserName = "writetorado@abv.bg",
                    Email = "writetorado@abv.bg",
                    FirstName = "AdminFirstName",
                    LastName = "AdminLastName",
                    EmailConfirmed = true,
                };

                var password = "123456";

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }
            }
        }
    }
}
