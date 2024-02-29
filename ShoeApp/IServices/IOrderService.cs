using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IOrderService
    {
        public Task<Response> Add(Order model);
        public Task<Response> AddMany(List<Order> model);
        public Task<Response> Update(Order model);
        public Task<Response> Delete(Guid Id);
        public Task<Order> Get(Guid Id);
        public Task<List<Order>> Gets();
    }
}
