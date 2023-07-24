using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Models;
using P010Store.Service.Abstract;
using P010Store.WebUI.Models;
using System.Diagnostics;

namespace P010Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Product> _service;

        private readonly IService<Carousel> _serviceCarousel;
        private readonly IService<Brand> _serviceBrand;

        public HomeController(IService<Product> service, IService<Carousel> serviceCarousel, IService<Brand> serviceBrand)
        {
            _service = service;
            _serviceCarousel = serviceCarousel;
            _serviceBrand = serviceBrand;
        }



        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel()
            {
                Products = await _service.GetAllAsync(p => p.IsHome),
                Carousels= await _serviceCarousel.GetAllAsync(),
                Brands = await _serviceBrand.GetAllAsync() 
            };

            // var model = await _service.GetAllAsync(p => p.IsHome);
            return View(model);
        }

        [Route("AccessDenied")]
        public IActionResult AccessDenied()
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