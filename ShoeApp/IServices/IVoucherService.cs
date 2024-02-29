using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IVoucherService
    {
        public Task<Response> Add(Voucher model);
        public Task<Response> AddMany(List<Voucher> model);
        public Task<Response> Update(Voucher model);
        public Task<Response> Delete(Guid Id);
        public Task<Voucher> Get(Guid Id);
        public Task<List<Voucher>> Gets();
    }
}
