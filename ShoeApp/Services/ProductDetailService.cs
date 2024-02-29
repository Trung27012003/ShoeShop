using ShoeApp.IServices;
using ShoeApp.Data;
using Microsoft.EntityFrameworkCore;
using ShoeApp.Models;

namespace ShoeApp.Services
{
    public class ProductDetailService: IProductDetailService
    {
        private readonly MyDbContext _context;

        public ProductDetailService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task<Response> Add(ProductDetail model)
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

        public async Task<Response> AddMany(List<ProductDetail> model)
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
                var obj = await _context.ProductDetail.FindAsync(Id);
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

        public async Task<ProductDetail> Get(Guid Id)
        {
            try
            {
                var lstObj = await _context.ProductDetail
                    .Include(c => c.Colors)
                      .Include(c => c.Size)
                    .ToListAsync();
                var obj = lstObj.FirstOrDefault(c => c.Id == Id);
                if (obj != null)
                {
                    return obj;
                }
                return new ProductDetail();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new ProductDetail();
            }
        }

        public async Task<List<ProductDetail>> Gets()
        {
            try
            {
                var lstObj = await _context.ProductDetail
                   .Include(c => c.Colors)
                     .Include(c => c.Size)
                   .ToListAsync();
                return lstObj;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<ProductDetail>();

            }
        }

        public async Task<Response> Update(ProductDetail model)
        {
            try
            {
                var obj = await _context.ProductDetail.FindAsync(model.Id);
                if (obj != null)
                {
                    obj.SizeId = model.SizeId;
                    obj.ColorId = model.ColorId;
                    obj.SKU = model.SKU;
                    obj.Quantity = model.Quantity;
                    obj.Price = model.Price;
                    obj.PriceSale = model.PriceSale;
                    obj.Create_At = model.Create_At;
                    obj.Update_At = model.Update_At;
                    obj.Description = model.Description;
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
