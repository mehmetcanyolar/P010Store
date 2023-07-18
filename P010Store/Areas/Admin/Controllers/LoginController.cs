using Microsoft.AspNetCore.Mvc;

namespace P010Store.WebUI.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
