using ShoeApp.IServices;

using ShoeApp.Data;
using Microsoft.EntityFrameworkCore;
using ShoeApp.Models;

namespace ShoeApp.Services
{
    public class RateService: IRateService
    {
        private readonly MyDbContext _context;

        public RateService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task<Response> Add(Rate model)
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

        public async Task<Response> AddMany(List<Rate> model)
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
                var obj = await _context.Rate.FindAsync(Id);
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

        public async Task<Rate> Get(Guid Id)
        {
            try
            {
                var obj = await _context.Rate.FindAsync(Id);
                if (obj != null)
                {
                    return obj;
                }
                return new Rate();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new Rate();
            }
        }

        public async Task<List<Rate>> Gets()
        {
            try
            {
                var lstObj = await _context.Rate.ToListAsync();
                return lstObj;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<Rate>();

            }
        }

        public async Task<Response> Update(Rate model)
        {
            try
            {
                var obj = await _context.Rate.FindAsync(model.Id);
                if (obj != null)
                {
                    obj.Reply = model.Reply;
                    obj.Status = model.Status;
                    obj.Content = model.Content;
                    obj.ImageUrl = model.ImageUrl;
                    obj.Rating = model.Rating;

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
