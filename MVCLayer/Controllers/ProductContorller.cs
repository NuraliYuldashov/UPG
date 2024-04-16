using Application.Interfaces; // Assuming your LaptopDto class is in this namespace
using Microsoft.AspNetCore.Mvc;

namespace MVCLayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILaptopService _laptopService;
        private readonly IGamingBuildsService _gamingBuildsService;

        public ProductController(ILaptopService laptopService,
                                 IGamingBuildsService gamingBuildsService)
        {
            _laptopService = laptopService;
            _gamingBuildsService = gamingBuildsService;
        }

        public async Task<IActionResult> Laptop(int id)
        {
            var item = await _laptopService.GetByIdAsync(id);
            return View(item);
        }

        public async Task<IActionResult> Sborka(int id)
        {
            var itme = await _gamingBuildsService.GetGamingBuildsByIdAsync(id);
            return View(itme);
        }
    }
}