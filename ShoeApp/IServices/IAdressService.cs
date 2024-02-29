using Microsoft.AspNetCore.Mvc;
using ShoeApp.Models;
using ShoeApp.Services;

namespace ShoeApp.IServices
{
    public interface IAdressService
    {
        public Task<Response> Add(Adress model);
        public Task<Response> AddMany(List<Adress> model);
        public Task<Response> Update(Adress model);
        public Task<Response> Delete(Guid Id);
        public Task<Adress> Get(Guid Id);
        public Task<List<Adress>> Gets();

    }
}
