namespace ShoeApp.Services;
using Microsoft.EntityFrameworkCore;
using ShoeApp.Data;
using ShoeApp.IServices;
using ShoeApp.Models;

public class OrderService: IOrderService
    {
        private readonly MyDbContext _context;

        public OrderService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
    public async Task<Response> Add(Order model)
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

    public async Task<Response> AddMany(List<Order> model)
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
            var obj = await _context.Order.FindAsync(Id);
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

    public async Task<Order> Get(Guid Id)
    {
        try
        {
            var lstObj = await _context.Order
                    .Include(c => c.User)
                    .Include(c=>c.OrderStatus)
                    .Include(c=>c.Voucher)
                    .ToListAsync();
            var obj = await _context.Order.FindAsync(Id);
            if (obj != null)
            {
                return obj;
            }
            return new Order();

        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
            return new Order();
        }
    }

    public async Task<List<Order>> Gets()
    {
        try
        {
            var lstObj = await _context.Order
                    .Include(c => c.User)
                    .Include(c => c.OrderStatus)
                    .Include(c => c.Voucher)
                    .ToListAsync();
            return lstObj;
        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
            return new List<Order>();

        }
    }

    public async Task<Response> Update(Order model)
    {
        try
        {

            var obj = await _context.Order.FindAsync(model.Id);
            if (obj != null)
            {
                obj.User_Reference_Id = model.User_Reference_Id;
                obj.OrderStatusId = model.OrderStatusId;
                obj.PaymentType = model.PaymentType;
                obj.VoucherId = model.VoucherId;
                obj.OrderCode = model.OrderCode;
                obj.RecipientName = model.RecipientName;
                obj.RecipientAddress = model.RecipientAddress;
                obj.RecipientPhone = model.RecipientPhone;
                obj.TotalAmout = model.TotalAmout;
                obj.VoucherValue = model.VoucherValue;
                obj.TotalAmoutAfterApplyingVoucher = model.TotalAmoutAfterApplyingVoucher;
                obj.ShippingFee = model.ShippingFee;
                obj.Create_Date = model.Create_Date;
                obj.Update_Date = model.Update_Date;
                obj.Ship_Date = model.Ship_Date;
                obj.Payment_Date = model.Payment_Date;
                obj.Delivery_Date = model.Delivery_Date;
                obj.Description = model.Description;
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