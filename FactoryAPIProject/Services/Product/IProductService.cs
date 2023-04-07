using FactoryAPIProject.Models;
using Microsoft.AspNetCore.Identity;

namespace FactoryAPIProject.Services.Product
{
    public interface IProductService
    {
        public Task<List<FactoryAPIProject.Models.Product>> GetAllProductAsync();
    }
}
