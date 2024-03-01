using Microsoft.AspNetCore.Mvc;
using ShoeApp.IServices;
using ShoeApp.Models;

namespace ShoeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _brandService.Gets());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Name)
        {

            var result = await _brandService.Add(new Brand() { BrandName = Name });
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid Id)
        {

            return View(await _brandService.Get(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid Id, string Name)
        {
            if (ModelState.IsValid)
            {
                var obj = await _brandService.Get(Id);
                obj.BrandName = Name;
                var result = await _brandService.Update(obj);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _brandService.Delete(Id);
                return RedirectToAction("Index");
        }
    }

}
