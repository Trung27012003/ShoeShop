using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IOrderStatusService
    {
        public Task<Response> Add(OrderStatus model);
        public Task<Response> AddMany(List<OrderStatus> model);
        public Task<Response> Update(OrderStatus model);
        public Task<Response> Delete(Guid Id);
        public Task<OrderStatus> Get(Guid Id);
        public Task<List<OrderStatus>> Gets();
    }
}
