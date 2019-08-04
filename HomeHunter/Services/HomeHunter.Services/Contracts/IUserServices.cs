using HomeHunter.Services.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IUserServices
    {
        bool IsUserEmailAuthenticated(string userId);

        Task<IEnumerable<UserIndexServiceModel>> GetAllUsersAsync();
    }
}
