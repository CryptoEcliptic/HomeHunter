using AutoMapper;
using HomeHunter.Models.ViewModels.Statistics;
using HomeHunter.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeHunter.App.Areas.Administration.Controllers
{
    public class HomeController : AdminController
    {
        private readonly IStatisticServices statisticServices;
        private readonly IMapper mapper;

        public HomeController(IStatisticServices statisticServices, IMapper mapper)
        {
            this.statisticServices = statisticServices;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Statistics()
        {
            var statisticsServiceModel = await this.statisticServices.GetAdministrationStatistics();
            var statisticsViewModel = this.mapper.Map<StatisticsViewModel>(statisticsServiceModel);

            return View(statisticsViewModel);
        }
    }
}