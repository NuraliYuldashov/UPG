using Application.Interfaces; // Assuming your LaptopDto class is in this namespace
using Microsoft.AspNetCore.Mvc;

namespace MVCLayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILaptopService _laptopService;

        public ProductController(ILaptopService laptopService)
        {
            _laptopService = laptopService;
        }

        public async Task<IActionResult> Laptop(int id)
        {
            var laptop = await _laptopService.GetByIdAsync(id);
            return View(laptop);
        }
    }
}