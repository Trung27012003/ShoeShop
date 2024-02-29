using Microsoft.AspNetCore.Identity;

public class CheckAccountStatusMiddleware
{
    private readonly RequestDelegate _next;

    public CheckAccountStatusMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, UserManager<ShoeApp.Models.User> userManager, SignInManager<ShoeApp.Models.User> signInManager)
    {
        if (context.User.Identity.IsAuthenticated) // check đăng nhập hợp lệ
        {
            var user = await userManager.GetUserAsync(context.User); 
            if (user == null || user.LockoutEnd!=null) // nếu không hợp lệ thì đăng xuất
            {
                await signInManager.SignOutAsync();
                context.Response.Redirect("/Identity/Account/Login");
                return;
            }
        }

        await _next(context);
    }
}
