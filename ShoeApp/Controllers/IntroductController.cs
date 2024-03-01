using Microsoft.AspNetCore.Mvc;

namespace ShoeApp.Controllers
{
    public class IntroductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
