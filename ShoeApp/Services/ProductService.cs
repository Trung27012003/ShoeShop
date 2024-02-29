using Microsoft.EntityFrameworkCore;
using ShoeApp.Data;
using ShoeApp.IServices;
using ShoeApp.Models;

namespace ShoeApp.Services
{
    public class ProductService: IProductService
    {
        private readonly MyDbContext _context;

        public ProductService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task<Response> Add(Product model)
        {
            try
            {
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return new Response { Messages = "Tạo thành công", StatusCode = 200, IsSuccess = true };
            }
            catch (Exception e)
            {
                return new Response { Messages = e.Message, StatusCode = 500, IsSuccess = false };
            }
        }

        public async Task<Response> AddMany(List<Product> model)
        {
            try
            {
                await _context.AddRangeAsync(model);
                await _context.SaveChangesAsync();
                return new Response { Messages = "Tạo thành công", StatusCode = 200, IsSuccess = true };
            }
            catch (Exception e)
            {
                return new Response { Messages = e.Message, StatusCode = 500, IsSuccess = false };
            }
        }

        public async Task<Response> Delete(Guid Id)
        {
            try
            {
                var obj = await _context.Product.FindAsync(Id);
                if (obj != null)
                {
                    _context.Remove(obj);
                    await _context.SaveChangesAsync();
                    return new Response { Messages = "Xoá thành công", StatusCode = 200, IsSuccess = true };
                }
                return new Response { Messages = "Xoá không thành công", StatusCode = 500, IsSuccess = false };
            }
            catch (Exception e)
            {
                return new Response { Messages = e.Message, StatusCode = 500, IsSuccess = false };
            }
        }

        public async Task<Product> Get(Guid Id)
        {
            try
            {
                var lstObj = await _context.Product
                    .Include(c => c.ProductDetails)
                      .Include(c => c.ProductDetails)
                        .ThenInclude(c => c.Colors)
                      .Include(c => c.ProductDetails)
                        .ThenInclude(c => c.Size)
                    .Include(c => c.Category)
                    .Include(c => c.Brand)
                    .Include(c => c.ProductImages)
                    .ToListAsync();
                var obj =  lstObj.FirstOrDefault(c=>c.Id==Id);
                if (obj != null)
                {
                    return obj;
                }
                return new Product();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new Product();
            }
        }

        public async Task<List<Product>> Gets()
        {
            try
            {
                var lstObj = await _context.Product
                    .Include(c=>c.ProductDetails)
                      .Include(c=>c.ProductDetails)
                        .ThenInclude(c=>c.Colors)
                      .Include(c=>c.ProductDetails)
                        .ThenInclude(c=>c.Size)
                    .Include(c=>c.Category)
                    .Include(c=>c.Brand)
                    .Include(c=>c.ProductImages)
                    .ToListAsync();
                return lstObj;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<Product>();

            }
        }

        public async Task<Response> Update(Product model)
        {
            try
            {
                var obj = await _context.Product.FindAsync(model.Id);
                if (obj != null)
                {
                    obj.CategoryId = model.CategoryId;
                    obj.BrandId = model.BrandId;
                    obj.ProductCode = model.ProductCode;
                    obj.ProductName = model.ProductName;
                    obj.AvailableQuantity = model.AvailableQuantity;
                    obj.Create_At = model.Create_At;
                    obj.Update_At = model.Update_At;
                    obj.Description = model.Description;
                    obj.Long_Description = model.Long_Description;
                    obj.Status = model.Status;
                    _context.Update(obj);
                    await _context.SaveChangesAsync();
                    return new Response { Messages = "Cập nhật thành công", StatusCode = 200, IsSuccess = true };
                }
                return new Response { Messages = "Cập nhật không thành công", StatusCode = 500, IsSuccess = false };
            }
            catch (Exception)
            {

                return new Response { Messages = "Cập nhật không thành công", StatusCode = 500, IsSuccess = false };
            }
        }
    }
}