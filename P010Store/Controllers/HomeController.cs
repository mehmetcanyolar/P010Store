using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Models;
using P010Store.Service.Abstract;
using System.Diagnostics;

namespace P010Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Product> _service;

        public HomeController(IService<Product> service)
        {
            _service = service;
        }

      

        public async Task<IActionResult> Index()
        {
            var model = await _service.GetAllAsync();
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