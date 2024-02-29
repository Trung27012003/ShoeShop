using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IBrandService
    {
        public Task<Response> Add(Brand model);
        public Task<Response> AddMany(List<Brand> model);
        public Task<Response> Update(Brand model);
        public Task<Response> Delete(Guid Id);
        public Task<Brand> Get(Guid Id);
        public Task<List<Brand>> Gets();
    }
}
