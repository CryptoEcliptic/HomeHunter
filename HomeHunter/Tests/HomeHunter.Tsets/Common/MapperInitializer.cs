using AutoMapper;
using HomeHunter.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.Tsets.Common
{
    public static class MapperInitializer
    {
        public static void InitializeMapper()
        {
            var services = new ServiceCollection();

            services.AddAutoMapper(typeof(HomeHunterProfile));
        }
    }
}
