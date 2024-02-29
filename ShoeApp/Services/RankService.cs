using ShoeApp.IServices;
using ShoeApp.Data;
using Microsoft.EntityFrameworkCore;
using ShoeApp.Models;

namespace ShoeApp.Services
{
    public class RankService: IRankService
    {
        private readonly MyDbContext _context;

        public RankService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task<Response> Add(Rank model)
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

        public async Task<Response> AddMany(List<Rank> model)
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
                var obj = await _context.Rank.FindAsync(Id);
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

        public async Task<Rank> Get(Guid Id)
        {
            try
            {
                var obj = await _context.Rank.FindAsync(Id);
                if (obj != null)
                {
                    return obj;
                }
                return new Rank();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new Rank();
            }
        }

        public async Task<List<Rank>> Gets()
        {
            try
            {
                var lstObj = await _context.Rank.ToListAsync();
                return lstObj;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<Rank>();

            }
        }

        public async Task<Response> Update(Rank model)
        {
            try
            {
                var obj = await _context.Rank.FindAsync(model.Id);
                if (obj != null)
                {
                    obj.PoinsMax = model.PoinsMax;
                    obj.PointsMin = model.PointsMin;
                    obj.Name = model.Name;
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
