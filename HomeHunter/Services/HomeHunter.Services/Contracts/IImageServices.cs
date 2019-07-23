using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeHunter.Services.Contracts
{
    public interface IImageServices
    {
        Task<bool> AddImageAsync(string url, string estateId);
    }
}
