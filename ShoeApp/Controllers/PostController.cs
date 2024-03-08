using Microsoft.AspNetCore.Mvc;
using ShoeApp.IServices;
using System.Net.WebSockets;

namespace ShoeApp.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var lstPosts = await _postService.Gets();
            lstPosts = lstPosts.Where(c=>c.Status).OrderBy(x => x.CreateAt).ToList();
            return View(lstPosts);
        }
    }
}
