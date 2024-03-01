using Microsoft.AspNetCore.Mvc;
using ShoeApp.IServices;

namespace ShoeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _orderService.Gets());
        }
    }
}
