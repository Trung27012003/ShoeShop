using Microsoft.EntityFrameworkCore;
using ShoeApp.Data;
using ShoeApp.IServices;
using ShoeApp.Models;

namespace ShoeApp.Services
{
    public class AdressService : IAdressService
    {
        private readonly MyDbContext _context;

        public AdressService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task<Response> Add(Adress model)
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

        public async Task<Response> AddMany(List<Adress> model)
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
                var obj = await _context.Adresses.FindAsync(Id);
                if (obj!=null)
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

        public async Task<Adress> Get(Guid Id)
        {
            try
            {
                var obj = await _context.Adresses.FindAsync(Id);
                if (obj!=null)
                {
                    return obj;
                }
                return new Adress();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new Adress();
            }
        }

        public async Task<List<Adress>> Gets()
        {
            try
            {
                var lstObj = await _context.Adresses.ToListAsync();
                return lstObj;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<Adress>();
                
            }
        }

        public async Task<Response> Update(Adress model)
        {
            try
            {
                var obj = await _context.Adresses.FindAsync(model.Id);
                if (obj!=null)
                {
                    obj.IsDefault = model.IsDefault;
                    obj.Ward = model.Ward;
                    obj.Province = model.Province;
                    obj.District = model.District;
                    obj.DescriptionAddress = model.DescriptionAddress;
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
