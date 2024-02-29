using ShoeApp.Data;
using Microsoft.EntityFrameworkCore;
using ShoeApp.IServices;
using ShoeApp.Models;

namespace ShoeApp.Services
{
    public class OrderStatusService: IOrderStatusService
    {
        private readonly MyDbContext _context;

        public OrderStatusService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task<Response> Add(OrderStatus model)
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

        public async Task<Response> AddMany(List<OrderStatus> model)
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
                var obj = await _context.OrderStatus.FindAsync(Id);
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

        public async Task<OrderStatus> Get(Guid Id)
        {
            try
            {
                var obj = await _context.OrderStatus.FindAsync(Id);
                if (obj != null)
                {
                    return obj;
                }
                return new OrderStatus();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new OrderStatus();
            }
        }

        public async Task<List<OrderStatus>> Gets()
        {
            try
            {
                var lstObj = await _context.OrderStatus.ToListAsync();
                return lstObj;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<OrderStatus>();

            }
        }

        public async Task<Response> Update(OrderStatus model)
        {
            try
            {
                var obj = await _context.OrderStatus.FindAsync(model.Id);
                if (obj != null)
                {
                    obj.OrderStatusName = model.OrderStatusName;
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
