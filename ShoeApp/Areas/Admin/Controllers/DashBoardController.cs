using Microsoft.AspNetCore.Mvc;
using ShoeApp.Helper;

namespace ShoeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AdminAreaAuthorization]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
