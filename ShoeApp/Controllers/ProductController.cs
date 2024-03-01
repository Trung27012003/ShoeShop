using Microsoft.AspNetCore.Mvc;

namespace ShoeApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
