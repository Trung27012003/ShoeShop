using Microsoft.AspNetCore.Mvc;
using ShoeApp.IServices;

namespace ShoeApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var lstProducts = await _productService.Gets();
            return View(lstProducts);
        }
    }
}
