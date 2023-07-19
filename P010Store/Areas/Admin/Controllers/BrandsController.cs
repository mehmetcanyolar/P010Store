using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P010Store.Entities;
using P010Store.Service.Abstract;
using P010Store.WebUI.Utils;

namespace P010Store.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize]
    public class BrandsController : Controller
    {
        private readonly IService<Brand> _service;

        public BrandsController(IService<Brand> service)
        {
            _service = service;
        }

        // GET: BrandsController
        public IActionResult Index()
        {
            var model= _service.GetAll();
            return View(model);
        }

        // GET: BrandsController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Brand brand, IFormFile? Logo)
        {
            if (ModelState.IsValid)
            {

                try
                {

                    brand.Logo = await FileHelper.FileLoaderAsync(Logo);
                    _service.Add(brand);
                    _service.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu! ");
                }
            }
          
            return View(brand);
        }


        //Modal Product Create nin Modal ı için Create2 ama redirectTo product


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2(Brand brand, IFormFile? Logo)
        {
            if (ModelState.IsValid)
            {

                try
                {

                    brand.Logo = await FileHelper.FileLoaderAsync(Logo);
                    _service.Add(brand);
                    _service.SaveChanges();

                    return RedirectToAction("Create","Products");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu! ");
                }
            }

            return View(brand);
        }



        // GET: BrandsController/Edit/5
        public async Task<IActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id); 
            return View(model);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, Brand brand,IFormFile? Logo, bool cbResimSil)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    if (cbResimSil)
                    {
                        FileHelper.FileRemover(brand.Logo);
                        brand.Logo=string.Empty; 
                    }

                 if(Logo != null)  brand.Logo = await FileHelper.FileLoaderAsync(Logo);

                  
                    _service.Update(brand);
                    _service.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu! ");
                }
            }

            return View(brand);

        }

        // GET: BrandsController/Delete/5
        public   async Task<IActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);

            return View(model);
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id,Brand brand)
        {
            try
            {
                _service.Delete(brand);
                _service.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu ! ");
            }
            return View(brand);
        }
    }
}
