using FactoryAPIProject.Models;

namespace FactoryAPIProject.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductAsync();

        List<Product> SeedProductData();

        Task AddProductAsync(Product product);
    }
}
