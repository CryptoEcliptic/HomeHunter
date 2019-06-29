using HomeHunter.Data;
using HomeHunter.Services.Contracts;
using System.Linq;

namespace HomeHunter.Services
{
    public class UsersService : IUsersService
    {
        private readonly HomeHunterDbContext context;

        public UsersService(HomeHunterDbContext context)
        {
            this.context = context;
        }

        public bool IsUserEmailAuthenticated(string userId)
        {
            var userFromDb = this.context.HomeHunterUsers.FirstOrDefault(x => x.Id == userId);

            if (userFromDb.EmailConfirmed == true)
            {
                return true;
            }

            return false;
        }
    }
}
