using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ShoeApp.IServices;
using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductDetailService _productDetailService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService;
        private readonly IColorService _colorService;
        private readonly ISizeService _sizeService;
        private List<IFormFile> _lstIFormFile;

        public ProductController(IProductService productService, IProductDetailService productDetailService, ICategoryService categoryService, IBrandService brandService, IProductImageService productImageService, ISizeService sizeService, IColorService colorService)
        {
            _productService = productService;
            _productDetailService = productDetailService;
            _brandService = brandService;
            _categoryService = categoryService;
            _productImageService = productImageService;
            _colorService = colorService;
            _sizeService = sizeService;
            _lstIFormFile = new List<IFormFile>();
        }
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _productService.Gets());
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName).Trim();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageList = HttpContext.Session.GetString("ImageList");
            var updatedImageList = imageList == null ? $"/images/{fileName}" : $"{imageList};/images/{fileName}";

            // Cập nhật danh sách ảnh trong Session
            HttpContext.Session.SetString("ImageList", updatedImageList);

            return Ok();
        }
        static string GetInitials(string input) // tạo productcode 
        {
            string[] words = input.Split(' ');
            string initials = "";

            foreach (string word in words)
            {
                if (!string.IsNullOrEmpty(word))
                {
                    initials += word[0];
                }
            }

            return initials.ToUpper(); // Chuyển thành chữ hoa
        }

        [HttpPost]
        public async Task<bool> RemoveImageAsync(string productImageId)
        {
            if (productImageId != null)
            {
                var id = Guid.Parse(productImageId);
                var result = await _productImageService.Delete(id);
                return result.IsSuccess;
            }
            return false;
        }
        public async Task<IActionResult> Create() // create product 
        {
            List<SelectListItem> ListCategoryitems = new List<SelectListItem>();
            // Giả sử myList là danh sách dữ liệu của bạn
            foreach (var obj in (await _categoryService.Gets()))
            {
                ListCategoryitems.Add(new SelectListItem()
                {
                    Text = obj.CategoryName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.ListCategoryitems = ListCategoryitems;
            List<SelectListItem> ListBranditems = new List<SelectListItem>();
            // Giả sử myList là danh sách dữ liệu của bạn
            foreach (var obj in await _brandService.Gets())
            {
                ListBranditems.Add(new SelectListItem()
                {
                    Text = obj.BrandName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.ListBranditems = ListBranditems;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product/*,string tags*/)
        {
            //var listTags = tags.Split(',').ToList(); // thêm vào chuỗi string 


            List<SelectListItem> ListCategoryitems = new List<SelectListItem>();
            // Giả sử myList là danh sách dữ liệu của bạn
            foreach (var obj in (await _categoryService.Gets()))
            {
                ListCategoryitems.Add(new SelectListItem()
                {
                    Text = obj.CategoryName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.ListCategoryitems = ListCategoryitems;
            List<SelectListItem> ListBranditems = new List<SelectListItem>();
            // Giả sử myList là danh sách dữ liệu của bạn
            foreach (var obj in await _brandService.Gets())
            {
                ListBranditems.Add(new SelectListItem()
                {
                    Text = obj.BrandName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.ListBranditems = ListBranditems;


            string imageList = HttpContext.Session.GetString("ImageList");
            if (ModelState.IsValid)
            {
                if (imageList == null)
                {
                    // Trả về view với thông báo lỗi nếu ModelState không hợp lệ hoặc không có tệp nào được tải lên
                    ModelState.AddModelError("", "Vui lòng thêm ảnh.");
                    return View(product);
                }
                product.Create_At = DateTime.Now;
                product.Description = "Description";
                product.Long_Description = product.Long_Description;
                product.ProductCode = GetInitials(product.ProductName) + DateTime.Now.ToString("yyMMddHHmmss");
                product.AvailableQuantity = -1;
                product.Status = false;
                var result = await _productService.Add(product);
                if (result.IsSuccess)
                {
                    List<string> imageListAsList = imageList.Split(';').ToList();
                    foreach (var item in imageListAsList)
                    {

                        var createProductImageResult = await _productImageService.Add(new ProductImage() { ProductId = product.Id, ImageUrl = item });
                        // Cập nhật đường dẫn hình ảnh cho sản phẩm

                    }
                    HttpContext.Session.Remove("ImageList");
                    return RedirectToAction("Index", "Product", new { @area = "Admin" });
                }

            }

            // Trả về view với thông báo lỗi nếu ModelState không hợp lệ hoặc không có tệp nào được tải lên
            ModelState.AddModelError("", "Vui lòng nhập đầy đủ các trường");
            return View(product);
        }

        public async Task<IActionResult> Edit(Guid productId) // edit product
        {
            List<SelectListItem> ListCategoryitems = new List<SelectListItem>();
            // Giả sử myList là danh sách dữ liệu của bạn
            foreach (var obj in (await _categoryService.Gets()))
            {
                ListCategoryitems.Add(new SelectListItem()
                {
                    Text = obj.CategoryName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.ListCategoryitems = ListCategoryitems;
            List<SelectListItem> ListBranditems = new List<SelectListItem>();
            // Giả sử myList là danh sách dữ liệu của bạn
            foreach (var obj in await _brandService.Gets())
            {
                ListBranditems.Add(new SelectListItem()
                {
                    Text = obj.BrandName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.ListBranditems = ListBranditems;
            return View(await _productService.Get(productId));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {

            List<SelectListItem> ListCategoryitems = new List<SelectListItem>();
            // Giả sử myList là danh sách dữ liệu của bạn
            foreach (var obj in (await _categoryService.Gets()))
            {
                ListCategoryitems.Add(new SelectListItem()
                {
                    Text = obj.CategoryName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.ListCategoryitems = ListCategoryitems;
            List<SelectListItem> ListBranditems = new List<SelectListItem>();
            // Giả sử myList là danh sách dữ liệu của bạn
            foreach (var obj in await _brandService.Gets())
            {
                ListBranditems.Add(new SelectListItem()
                {
                    Text = obj.BrandName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.ListBranditems = ListBranditems;
            string imageList = HttpContext.Session.GetString("ImageList");
            if (ModelState.IsValid)
            {

                var productDb = await _productService.Get(product.Id);
                productDb.CategoryId = product.CategoryId;
                productDb.BrandId = product.BrandId;
                productDb.Update_At = DateTime.Now;
                productDb.Long_Description = product.Long_Description;
                productDb.Status = false;
                if (imageList == null && productDb.ProductImages.Count() == 0) // check ảnh
                {
                    // Trả về view với thông báo lỗi nếu ModelState không hợp lệ hoặc không có tệp nào được tải lên
                    ModelState.AddModelError("", "Ảnh sản phẩm tối thiểu là 1, vui lòng thêm ảnh.");
                    return View(product);
                }
                if (imageList != null)
                {
                    List<string> imageListAsList = imageList.Split(';').ToList();
                    foreach (var item in imageListAsList)
                    {
                        var createProductImageResult = await _productImageService.Add(new ProductImage() { ProductId = product.Id, ImageUrl = item });
                        // Cập nhật đường dẫn hình ảnh cho sản phẩm

                    }
                }
                var result = await _productService.Update(productDb);
                if (result.IsSuccess)
                {

                    HttpContext.Session.Remove("ImageList");
                    return RedirectToAction("Index", "Product", new { @area = "Admin" });
                }

            }

            // Trả về view với thông báo lỗi nếu ModelState không hợp lệ hoặc không có tệp nào được tải lên
            ModelState.AddModelError("", "Vui lòng nhập đầy đủ các trường");
            return RedirectToAction("Edit", null, new { @productId = product.Id });
        }
        public async Task<IActionResult> ChangeStatus(Guid productId)
        {

            var product = await _productService.Get(productId);
            if (product.Status == true)
            {
                product.Status = false;
            }
            else
            {
                product.Status = true;
            }
            _productService.Update(product);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ProductVariant(Guid productId) // get list productdetail
        {
            var lstProductDetail = await _productDetailService.Gets();

            ViewBag.productId = productId;
            var lstProductDetailFind = lstProductDetail.Where(c => c.ProductId == productId);
            return View(lstProductDetailFind);
        }
        public async Task<IActionResult> CreatePD(Guid productId) // create productdetail
        {
            List<SelectListItem> ListSizeitems = new List<SelectListItem>();
            foreach (var obj in (await _sizeService.Gets()))
            {
                ListSizeitems.Add(new SelectListItem()
                {
                    Text = obj.SizeName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.productId = productId;
            ViewBag.ListSizeitems = ListSizeitems;
            List<SelectListItem> ListColoritems = new List<SelectListItem>();
            foreach (var obj in (await _colorService.Gets()))
            {
                ListColoritems.Add(new SelectListItem()
                {
                    Text = obj.ColorName,
                    Value = obj.ColorId.ToString()
                });
            }
            ViewBag.ListColoritems = ListColoritems;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePD(ProductDetail productDetail)
        {
            List<SelectListItem> ListSizeitems = new List<SelectListItem>();
            foreach (var obj in (await _sizeService.Gets()))
            {
                ListSizeitems.Add(new SelectListItem()
                {
                    Text = obj.SizeName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.ListSizeitems = ListSizeitems;
            List<SelectListItem> ListColoritems = new List<SelectListItem>();
            foreach (var obj in (await _colorService.Gets()))
            {
                ListColoritems.Add(new SelectListItem()
                {
                    Text = obj.ColorName,
                    Value = obj.ColorId.ToString()
                });
            }
            ViewBag.ListColoritems = ListColoritems;
            if (ModelState.IsValid)
            {
                var product = await _productService.Get(productDetail.ProductId);
                var color = await _colorService.Get(productDetail.ColorId);
                var size = await _sizeService.Get(productDetail.SizeId);
                productDetail.Create_At = DateTime.Now;
                productDetail.Description = "Description";
                productDetail.SKU = product.ProductCode + "-" + color.ColorName.Trim().Replace(" ", "_").ToUpper() + "-" + size.SizeName.Trim().Replace(" ", "_").ToUpper();
                productDetail.Status = false;
                var result = await _productDetailService.Add(productDetail);
                if (result.IsSuccess)
                {
                    return RedirectToAction("ProductVariant", null, new { @productId = productDetail.ProductId });
                }
            }
            ModelState.AddModelError("", "Vui lòng nhập đầy đủ các trường");
            return View(productDetail);
        }
        public async Task<IActionResult> EditPD(Guid productPD) // edit productdetail
        {
            List<SelectListItem> ListSizeitems = new List<SelectListItem>();
            foreach (var obj in (await _sizeService.Gets()))
            {
                ListSizeitems.Add(new SelectListItem()
                {
                    Text = obj.SizeName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.ListSizeitems = ListSizeitems;
            List<SelectListItem> ListColoritems = new List<SelectListItem>();
            foreach (var obj in (await _colorService.Gets()))
            {
                ListColoritems.Add(new SelectListItem()
                {
                    Text = obj.ColorName,
                    Value = obj.ColorId.ToString()
                });
            }
            ViewBag.ListColoritems = ListColoritems;
            return View(await _productDetailService.Get(productPD));
        }
        [HttpPost]
        public async Task<IActionResult> EditPD(ProductDetail productDetail)
        {
            List<SelectListItem> ListSizeitems = new List<SelectListItem>();
            foreach (var obj in (await _sizeService.Gets()))
            {
                ListSizeitems.Add(new SelectListItem()
                {
                    Text = obj.SizeName,
                    Value = obj.Id.ToString()
                });
            }
            ViewBag.ListSizeitems = ListSizeitems;
            List<SelectListItem> ListColoritems = new List<SelectListItem>();
            foreach (var obj in (await _colorService.Gets()))
            {
                ListColoritems.Add(new SelectListItem()
                {
                    Text = obj.ColorName,
                    Value = obj.ColorId.ToString()
                });
            }
            ViewBag.ListColoritems = ListColoritems;
            if (ModelState.IsValid)
            {
                var productDT = await _productDetailService.Get(productDetail.Id);
                productDT.SizeId = productDetail.SizeId;
                productDT.ColorId = productDetail.ColorId;
                productDT.Quantity = productDetail.Quantity;
                productDT.Price = productDetail.Price;
                productDT.PriceSale = productDetail.PriceSale;
                productDT.Update_At = DateTime.Now;
                productDT.Description = productDetail.Description;
                productDT.Status = true;
                var result = await _productDetailService.Update(productDT);
                if (result.IsSuccess)
                {
                    return RedirectToAction("ProductVariant", null, new { @productId = productDetail.ProductId });
                }

            }

            // Trả về view với thông báo lỗi nếu ModelState không hợp lệ hoặc không có tệp nào được tải lên
            ModelState.AddModelError("", "Vui lòng nhập đầy đủ các trường");
            return RedirectToAction("EditPD", null, new { @productPD = productDetail.Id });
        }
        public async Task<IActionResult> ChangeStatusPD(Guid productPD) // change status productdetail
        {

            var product = await _productDetailService.Get(productPD);
            if (product.Status == true)
            {
                product.Status = false;
            }
            else
            {
                product.Status = true;
            }
            _productDetailService.Update(product);
            return RedirectToAction("ProductVariant", null, new { @productId = product.ProductId });

        }

    }
}
