using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IColorService
    {
        public Task<Response> Add(Color model);
        public Task<Response> AddMany(List<Color> model);
        public Task<Response> Update(Color model);
        public Task<Response> Delete(Guid Id);
        public Task<Color> Get(Guid Id);
        public Task<List<Color>> Gets();
    }
}
