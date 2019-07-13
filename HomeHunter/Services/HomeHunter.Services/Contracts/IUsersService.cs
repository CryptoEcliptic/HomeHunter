using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Services.Contracts
{
    public interface IUsersService
    {
        bool IsUserEmailAuthenticated(string userId);
    }
}
