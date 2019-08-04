using AutoMapper;
using HomeHunter.Data;
using HomeHunter.Services.Contracts;
using HomeHunter.Services.Models.User;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeHunter.Services
{
    public class UserServices : IUserServices
    {
        private readonly HomeHunterDbContext context;
        private readonly IMapper mapper;

        public UserServices(HomeHunterDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
    }
}
