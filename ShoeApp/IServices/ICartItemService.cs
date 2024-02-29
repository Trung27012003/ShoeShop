using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface ICartItemService
    {
        public Task<Response> Add(CartItem model);
        public Task<Response> AddMany(List<CartItem> model);
        public Task<Response> Update(CartItem model);
        public Task<Response> Delete(Guid Id);
        public Task<CartItem> Get(Guid Id);
        public Task<List<CartItem>> Gets();
    }
}
