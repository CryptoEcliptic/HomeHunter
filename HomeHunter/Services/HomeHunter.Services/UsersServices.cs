using HomeHunter.Data;
using HomeHunter.Services.Contracts;
using System.Linq;

namespace HomeHunter.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly HomeHunterDbContext context;

        public UsersServices(HomeHunterDbContext context)
        {
            this.context = context;
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
    }
}
