using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IRateService
    {
        public Task<Response> Add(Rate model);
        public Task<Response> AddMany(List<Rate> model);
        public Task<Response> Update(Rate model);
        public Task<Response> Delete(Guid Id);
        public Task<Rate> Get(Guid Id);
        public Task<List<Rate>> Gets();
    }
}
