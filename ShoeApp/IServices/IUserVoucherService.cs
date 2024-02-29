using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IUserVoucherService
    {
        public Task<Response> Add(UserVoucher model);
        public Task<Response> AddMany(List<UserVoucher> model);
        public Task<Response> Update(UserVoucher model);
        public Task<Response> Delete(Guid Id);
        public Task<UserVoucher> Get(Guid Id);
        public Task<List<UserVoucher>> Gets();
    }
}
