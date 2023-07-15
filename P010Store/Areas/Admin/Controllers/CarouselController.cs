using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using P010Store.Entities;
using P010Store.Service.Abstract;
using P010Store.WebUI.Utils;

namespace P010Store.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarouselController : Controller
    {
        private readonly IService<Carousel> _service;

        public CarouselController(IService<Carousel> service)
        {
            _service = service;
        }






        // GET: CarouselController
        public IActionResult Index()
        {
            var model =  _service.GetAll();
            return View(model);
        }

        // GET: CarouselController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: CarouselController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarouselController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Carousel carousel,IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(Image is not null) carousel.Image = await FileHelper.FileLoaderAsync(Image);

                    await _service.AddAsync(carousel);
                    await _service.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu! ");
                }
            }
          
            return View(carousel);
        }

        // GET: CarouselController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);

            return View(model);
        }

        // POST: CarouselController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, Carousel carousel, IFormFile? Image, bool cbResimSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(cbResimSil== true)
                    {
                        FileHelper.FileRemover(carousel.Image);
                        carousel.Image = string.Empty;

                    }

                    if (Image is not null) carousel.Image = await FileHelper.FileLoaderAsync(Image);

                   _service.Update(carousel);
                    await _service.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu! ");
                }
            }

            return View(carousel);
        }

        // GET: CarouselController/Delete/5
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CarouselController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Carousel carousel )
        {
            try
            {
                _service.Delete(carousel);
                _service.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu! ");
                
            }
            return View();
        }
    }
}
