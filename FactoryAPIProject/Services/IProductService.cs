using FactoryAPIProject.Models;

namespace FactoryAPIProject.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAllProductAsync();

        public List<Product> SeedProductData();

        public Task AddProductAsync(ProductModel product);
    }
}
