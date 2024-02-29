using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface ISizeService
    {
        public Task<Response> Add(Size model);
        public Task<Response> AddMany(List<Size> model);
        public Task<Response> Update(Size model);
        public Task<Response> Delete(Guid Id);
        public Task<Size> Get(Guid Id);
        public Task<List<Size>> Gets();
    }
}
