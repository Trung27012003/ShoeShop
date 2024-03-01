using Microsoft.AspNetCore.Mvc;
using ShoeApp.IServices;
using ShoeApp.Models;

namespace ShoeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        private ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _sizeService.Gets());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Name)
        {

            var result = await _sizeService.Add(new Size() { SizeName = Name });
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid Id)
        {

            return View(await _sizeService.Get(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid Id, string Name)
        {

                var obj = await _sizeService.Get(Id);
                obj.SizeName = Name;
                var result = await _sizeService.Update(obj);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _sizeService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
