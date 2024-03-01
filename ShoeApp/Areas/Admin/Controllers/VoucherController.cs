using Microsoft.AspNetCore.Mvc;
using ShoeApp.IServices;

namespace ShoeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VoucherController : Controller
    {
        private IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _voucherService.Gets());
        }
    }
}
