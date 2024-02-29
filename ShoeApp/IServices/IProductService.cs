using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IProductService
    {
        public Task<Response> Add(Product model);
        public Task<Response> AddMany(List<Product> model);
        public Task<Response> Update(Product model);
        public Task<Response> Delete(Guid Id);
        public Task<Product> Get(Guid Id);
        public Task<List<Product>> Gets();
    }
}
