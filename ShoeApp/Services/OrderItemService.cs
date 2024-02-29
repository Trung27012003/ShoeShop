using Microsoft.EntityFrameworkCore;
using ShoeApp.Data;
using ShoeApp.IServices;
using ShoeApp.Models;

namespace ShoeApp.Services
{
    public class OrderItemService: IOrderItemService
    {
        private readonly MyDbContext _context;

        public OrderItemService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task<Response> Add(OrderItem model)
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

        public async Task<Response> AddMany(List<OrderItem> model)
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
                var obj = await _context.OrderItem.FindAsync(Id);
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

        public async Task<OrderItem> Get(Guid Id)
        {
            try
            {
                var obj = await _context.OrderItem.FindAsync(Id);
                if (obj != null)
                {
                    return obj;
                }
                return new OrderItem();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new OrderItem();
            }
        }

        public async Task<List<OrderItem>> Gets()
        {
            try
            {
                var lstObj = await _context.OrderItem.ToListAsync();
                return lstObj;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<OrderItem>();

            }
        }

        public async Task<Response> Update(OrderItem model)
        {
            try
            {
                var obj = await _context.OrderItem.FindAsync(model.Id);
                if (obj != null)
                {
                    obj.Quantity = model.Quantity;
                    obj.Price = model.Price;
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
