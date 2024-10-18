using Microsoft.AspNetCore.Mvc;
using Ogani.BLL.UI.Services.Contracts;
using Ogani.DAL.DataContext.Entities;
using Ogani.MVC.Models;
using System.Diagnostics;

namespace Ogani.MVC.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var homeViewModel = await _homeService.GetHomeViewModelAsync();
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
