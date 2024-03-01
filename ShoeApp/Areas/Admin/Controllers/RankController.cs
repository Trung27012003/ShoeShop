using Microsoft.AspNetCore.Mvc;
using ShoeApp.IServices;
using ShoeApp.Models;

namespace ShoeApp.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class RankController : Controller
    {
        private IRankService _rankService;

        public RankController(IRankService brandService)
        {
            _rankService = brandService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var ranks = await _rankService.Gets();
            var ranksOrder = ranks.OrderBy(c => c.PointsMin);
            return View(ranksOrder);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string Name,string PointsMin,string PoinsMax)
        {

            var result = await _rankService.Add(new Rank() { Name = Name ,PointsMin = int.Parse(PointsMin),PoinsMax = int.Parse(PoinsMax) });
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(Guid Id)
        {

            return View(await _rankService.Get(Id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Guid Id, string Name, string PointsMin, string PoinsMax)
        {
            if (ModelState.IsValid)
            {
                var obj = await _rankService.Get(Id);
                obj.Name = Name;
                obj.PoinsMax = int.Parse(PoinsMax);
                obj.PointsMin = int.Parse(PointsMin);
                var result = await _rankService.Update(obj);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _rankService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
