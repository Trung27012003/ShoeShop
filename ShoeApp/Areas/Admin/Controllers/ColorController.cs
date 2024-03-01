using Microsoft.AspNetCore.Mvc;
using ShoeApp.IServices;
using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _colorService.Gets());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Name,string Code)
        {

            var result = await _colorService.Add(new Color() { ColorName = Name,ColorCode = Code });
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid Id)
        {

            return View(await _colorService.Get(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid Id, string Name,string Code)
        {

            var obj = await _colorService.Get(Id);
            obj.ColorName = Name;
            obj.ColorCode = Code;
            var result = await _colorService.Update(obj);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _colorService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
