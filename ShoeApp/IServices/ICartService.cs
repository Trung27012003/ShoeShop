using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface ICartService
    {
        public Task<Response> Add(Cart model);
        public Task<Response> AddMany(List<Cart> model);
        public Task<Response> Update(Cart model);
        public Task<Response> Delete(Guid Id);
        public Task<Cart> Get(Guid Id);
        public Task<List<Cart>> Gets();
    }
}
