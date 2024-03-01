using Microsoft.EntityFrameworkCore;
using ShoeApp.Data;
using ShoeApp.IServices;
using ShoeApp.Models;

namespace ShoeApp.Services
{
    public class VoucherService: IVoucherService
    {
        private readonly MyDbContext _context;

        public VoucherService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task<Response> Add(Voucher model)
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

        public async Task<Response> AddMany(List<Voucher> model)
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
                var obj = await _context.Voucher.FindAsync(Id);
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

        public async Task<Voucher> Get(Guid Id)
        {
            try
            {
                var obj = await _context.Voucher.FindAsync(Id);
                if (obj != null)
                {
                    return obj;
                }
                return new Voucher();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new Voucher();
            }
        }

        public async Task<List<Voucher>> Gets()
        {
            try
            {
                var lstObj = await _context.Voucher.ToListAsync();
                return lstObj;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<Voucher>();

            }
        }

        public async Task<Response> Update(Voucher model)
        {
            try
            {
                var obj = await _context.Voucher.FindAsync(model.Id);
                if (obj != null)
                {
                    obj.VoucherCode = model.VoucherCode;
                    obj.Title = model.Title;
                    obj.Quantity = model.Quantity;
                    obj.Value = model.Value;
                    obj.Discount_Type = model.Discount_Type;
                    obj.Minimum_order_value = model.Minimum_order_value;
                    obj.MaxDiscountAmount = model.MaxDiscountAmount;
                    obj.Description = model.Description;
                    obj.Create_Date = model.Create_Date;
                    obj.Start_Date = model.Start_Date;
                    obj.End_Date = model.End_Date;
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
