using Microsoft.EntityFrameworkCore;
using ShoeApp.Data;
using ShoeApp.IServices;
using ShoeApp.Models;

namespace ShoeApp.Services
{
    public class PostService: IPostService
    {
        private readonly MyDbContext _context;

        public PostService(MyDbContext myDbContext)
        {
            _context = myDbContext;
        }
        public async Task<Response> Add(Post model)
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

        public async Task<Response> AddMany(List<Post> model)
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
                var obj = await _context.Posts.FindAsync(Id);
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

        public async Task<Post> Get(Guid Id)
        {
            try
            {
                var obj = await _context.Posts.FindAsync(Id);
                if (obj != null)
                {
                    return obj;
                }
                return new Post();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new Post();
            }
        }

        public async Task<List<Post>> Gets()
        {
            try
            {
                var lstObj = await _context.Posts.ToListAsync();
                return lstObj;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return new List<Post>();

            }
        }

        public async Task<Response> Update(Post model)
        {
            try
            {
                var obj = await _context.Posts.FindAsync(model.Id);
                if (obj != null)
                {
                    obj.Tittle = model.Tittle;
                    obj.TittleImage = model.TittleImage;
                    obj.Contents = model.Contents;
                    obj.CreateAt = model.CreateAt;
                    obj.UpdateAt = model.UpdateAt;
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
}