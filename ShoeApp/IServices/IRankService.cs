using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IRankService
    {
        public Task<Response> Add(Rank model);
        public Task<Response> AddMany(List<Rank> model);
        public Task<Response> Update(Rank model);
        public Task<Response> Delete(Guid Id);
        public Task<Rank> Get(Guid Id);
        public Task<List<Rank>> Gets();
    }
}
