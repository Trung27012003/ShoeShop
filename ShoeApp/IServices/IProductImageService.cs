using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IProductImageService
    {
        public Task<Response> Add(ProductImage model);
        public Task<Response> AddMany(List<ProductImage> model);
        public Task<Response> Update(ProductImage model);
        public Task<Response> Delete(Guid Id);
        public Task<ProductImage> Get(Guid Id);
        public Task<List<ProductImage>> Gets();
    }
}
