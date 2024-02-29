using Microsoft.EntityFrameworkCore;
using ShoeApp.Data;
using ShoeApp.IServices;
using ShoeApp.Models;

namespace ShoeApp.Services
{
    public class ProductImageService: IProductImageService
    {
        private readonly MyDbContext _context;

        public ProductImageService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task<Response> Add(ProductImage model)
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

        public async Task<Response> AddMany(List<ProductImage> model)
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
                var obj = await _context.ProductImage.FindAsync(Id);
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

        public async Task<ProductImage> Get(Guid Id)
        {
            try
            {
                var obj = await _context.ProductImage.FindAsync(Id);
                if (obj != null)
                {
                    return obj;
                }
                return new ProductImage();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new ProductImage();
            }
        }

        public async Task<List<ProductImage>> Gets()
        {
            try
            {
                var lstObj = await _context.ProductImage.ToListAsync();
                return lstObj;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<ProductImage>();

            }
        }

        public async Task<Response> Update(ProductImage model)
        {
            try
            {
                var obj = await _context.ProductImage.FindAsync(model.Id);
                if (obj != null)
                {
                    obj.ImageUrl = model.ImageUrl;
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
