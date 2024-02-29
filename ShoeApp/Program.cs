using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShoeApp.Data;
using ShoeApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using ShoeApp.Helper;
using ShoeApp.Services;
using ShoeApp.IServices;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddDbContext<MyDbContext>(options =>
             options.UseSqlServer(
                 builder.Configuration.GetConnectionString("DefaultConnection")));

// add identity
builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MyDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

// add dependency injection
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<IAdressService,AdressService>();
builder.Services.AddScoped<IBrandService,BrandService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService,CartService>();
builder.Services.AddScoped<ICartItemService,CartItemService>();
builder.Services.AddScoped<IColorService,ColorService>();
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped<IOrderStatusService,OrderStatusService>();
builder.Services.AddScoped<IOrderItemService,OrderItemService>();
builder.Services.AddScoped<IPostService,PostService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IRankService, RankService>();
builder.Services.AddScoped<IRateService, RateService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IUserVoucherService, UserVoucherService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();




// add authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreateV1", policy => policy.RequireClaim("Create", "True"));
    options.AddPolicy("EditV1", policy => policy.RequireClaim("Edit", "True"));
    options.AddPolicy("DeleteV1", policy => policy.RequireClaim("Delete", "True"));
});

// add razor page
builder.Services.AddRazorPages();
// add session
builder.Services.AddSession();

builder.Services.AddControllersWithViews();

//add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})

  .AddCookie()
  .AddGoogle(options =>
  {     // Đọc thông tin Authentication:Google
      IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
      // Thiết lập ClientID và ClientSecret để truy cập API google
      options.ClientId = googleAuthNSection["ClientId"];
      options.ClientSecret = googleAuthNSection["ClientSecret"];
      // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
      options.CallbackPath = "/signin-google";
  })
//.AddFacebook(options =>
//  {     // Đọc thông tin Authentication:facebook
//      IConfigurationSection facebookAuthNSection = builder.Configuration.GetSection("Authentication:Facebook");
//      // Thiết lập ClientID và ClientSecret để truy cập API google
//      options.ClientId = facebookAuthNSection["ClientId"];
//      options.ClientSecret = facebookAuthNSection["ClientSecret"];
//      // Cấu hình Url callback lại từ Google (không thiết lập thì mặc định là /signin-google)
//  })

;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
// use session
app.UseSession();

//use authen + author
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CheckAccountStatusMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.MapRazorPages();
app.Run();
