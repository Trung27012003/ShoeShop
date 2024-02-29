using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IProductDetailService
    {
        public Task<Response> Add(ProductDetail model);
        public Task<Response> AddMany(List<ProductDetail> model);
        public Task<Response> Update(ProductDetail model);
        public Task<Response> Delete(Guid Id);
        public Task<ProductDetail> Get(Guid Id);
        public Task<List<ProductDetail>> Gets();
    }
}
