using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface ICategoryService
    {
        public Task<Response> Add(Category model);
        public Task<Response> AddMany(List<Category> model);
        public Task<Response> Update(Category model);
        public Task<Response> Delete(Guid Id);
        public Task<Category> Get(Guid Id);
        public Task<List<Category>> Gets();
    }
}
