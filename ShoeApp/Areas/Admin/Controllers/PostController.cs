using Microsoft.AspNetCore.Mvc;
using ShoeApp.IServices;
using ShoeApp.Models;
using ShoeApp.Services;
using System.Net.WebSockets;
using System.Security.Claims;

namespace ShoeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<IActionResult> IndexAsync()
        {

            return View(await _postService.Gets());
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName).Trim();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/post", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageList = HttpContext.Session.GetString("TitleImage");
            if (imageList!=null)
            {
                HttpContext.Session.Remove("TitleImage");
            }
            var updatedImageList =  $"/images/post/{fileName}" ;
            // Cập nhật danh sách ảnh trong Session
            HttpContext.Session.SetString("TitleImage", updatedImageList);

            return Ok();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Post post)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Content("chuadangnhap");
            }
            string TitleImage = HttpContext.Session.GetString("TitleImage");
            if (TitleImage==null)
            {
                // Trả về view với thông báo lỗi nếu ModelState không hợp lệ hoặc không có tệp nào được tải lên
                ModelState.AddModelError("", "Vui lòng thêm ảnh.");
                return View(post);
            }
            if (ModelState.IsValid)
            {
                post.CreateAt = DateTime.Now;
                post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                post.TittleImage = TitleImage;
                var createPostResult = await _postService.Add(post);
                if (createPostResult.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(post);
        }
        public async Task<IActionResult> Edit(Guid postId)
        {
            return View( await _postService.Get(postId));
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(Post post)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Content("chuadangnhap");
            }
            if (ModelState.IsValid)
            {
                var postDb = await _postService.Get(post.Id);
                postDb.UpdateAt = DateTime.Now;
                postDb.Contents = post.Contents;
                postDb.Status = post.Status;
                postDb.Tittle = post.Tittle;
                postDb.Description = post.Description;
                var createPostResult = await _postService.Update(postDb);
                if (createPostResult.IsSuccess)
                {
                   return RedirectToAction("Index");
                }
            }
            return View(post);
        }
        public async Task<IActionResult> ChangeStatus(Guid postId) // change status productdetail
        {

            var post = await _postService.Get(postId);
            if (post.Status == true)
            {
                post.Status = false;
            }
            else
            {
                post.Status = true;
            }
            _postService.Update(post);
            return RedirectToAction("index");

        }
    }
}
