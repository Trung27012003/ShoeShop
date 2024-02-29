using ShoeApp.IServices;

using ShoeApp.Data;
using Microsoft.EntityFrameworkCore;
using ShoeApp.Models;

namespace ShoeApp.Services
{
    public class UserVoucherService: IUserVoucherService
    {
        private readonly MyDbContext _context;

        public UserVoucherService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task<Response> Add(UserVoucher model)
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

        public async Task<Response> AddMany(List<UserVoucher> model)
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
                var obj = await _context.VoucherUser.FindAsync(Id);
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

        public async Task<UserVoucher> Get(Guid Id)
        {
            try
            {
                var obj = await _context.VoucherUser.FindAsync(Id);
                if (obj != null)
                {
                    return obj;
                }
                return new UserVoucher();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new UserVoucher();
            }
        }

        public async Task<List<UserVoucher>> Gets()
        {
            try
            {
                var lstObj = await _context.VoucherUser.ToListAsync();
                return lstObj;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<UserVoucher>();

            }
        }

        public async Task<Response> Update(UserVoucher model)
        {
            try
            {
                var obj = await _context.VoucherUser.FindAsync(model.Id);
                if (obj != null)
                {
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
