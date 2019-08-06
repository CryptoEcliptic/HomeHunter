using HomeHunter.Domain;
using HomeHunter.Services.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IUserServices
    {
        bool IsUserEmailAuthenticated(string userId);

        Task<IEnumerable<UserIndexServiceModel>> GetAllUsersAsync();

        Task<UserReturnCreateServiceModel> CreateUser(UserCreateServiceModel model);

        Task<bool> SendVerificationEmail(string callBackUrl, string email);

        Task<UserDetailsServiceModel> GetUserDetailsAsync(string userId);

        Task<bool> SoftDeleteUserAsync(string userId);

        Task<HomeHunterUser> GetUserById(string userId);
    }
}
