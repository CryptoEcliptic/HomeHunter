using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Services.Contracts
{
    public interface IUsersServices
    {
        bool IsUserEmailAuthenticated(string userId);
    }
}
