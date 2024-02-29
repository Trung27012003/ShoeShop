using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IPostService
    {
        public Task<Response> Add(Post model);
        public Task<Response> AddMany(List<Post> model);
        public Task<Response> Update(Post model);
        public Task<Response> Delete(Guid Id);
        public Task<Post> Get(Guid Id);
        public Task<List<Post>> Gets();
    }
}
