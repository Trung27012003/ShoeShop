using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IOrderItemService
    {
        public Task<Response> Add(OrderItem model);
        public Task<Response> AddMany(List<OrderItem> model);
        public Task<Response> Update(OrderItem model);
        public Task<Response> Delete(Guid Id);
        public Task<OrderItem> Get(Guid Id);
        public Task<List<OrderItem>> Gets();
    }
}
