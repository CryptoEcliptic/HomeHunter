using AutoMapper;
using HomeHunter.App.Models.HeatingSystem;
using HomeHunter.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeHunter.MapperConfugurationServices
{
    class HomeHunterProfile : Profile
    {
        public HomeHunterProfile()
        {
            this.CreateMap<HeatingSystem, HeatingSystemVewModel>();
        }
    }
}
