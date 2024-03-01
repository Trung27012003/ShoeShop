using Microsoft.AspNetCore.Mvc;
using ShoeApp.IServices;
using ShoeApp.Models;

namespace ShoeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _categoryService.Gets());
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Name)
        {

            var result = await _categoryService.Add(new Category() { CategoryName = Name });
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid Id)
        {

            return View(await _categoryService.Get(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid Id, string Name)
        {

                var obj = await _categoryService.Get(Id);
                obj.CategoryName = Name;
                var result = await _categoryService.Update(obj);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _categoryService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
