using FactoryAPIProject.Data;
using FactoryAPIProject.Data.UnitOfWorks;
using FactoryAPIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FactoryAPIProject.Services
{
    public class ProductService : IProductService
    {

        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddProductAsync(Product product)
        {
            await unitOfWork.GetRepository<Product>().AddAsync(product);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            var countProduct = await unitOfWork.GetRepository<Product>().GetAllAsync();
            if (!countProduct.Any())
            {
                await unitOfWork.GetRepository<Product>().AddRangeAsync(SeedProductData());
                await unitOfWork.SaveAsync();
                countProduct = await unitOfWork.GetRepository<Product>().GetAllAsync();
            }
            return countProduct;
        }

        public List<Product> SeedProductData()
        {
            List<Product> products = new List<Product>()
            {
                new Product() {ProductName = "erik", Price=12},
                new Product() {ProductName = "apple", Price=14},
                new Product() {ProductName = "orange", Price=11},
                new Product() {ProductName = "grape", Price=13},
                new Product() {ProductName = "cake", Price=13},
                new Product() {ProductName = "chocolatte", Price=15},
                new Product() {ProductName = "bread", Price=103},
                new Product() {ProductName = "chips", Price=17},
                new Product() {ProductName = "patato", Price=19}
            };
            return products;
        }

        public async Task DeleteProductAsync(int id)
        {
            await unitOfWork.GetRepository<Product>().DeleteAsync(id);
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await unitOfWork.GetRepository<Product>().GetByIdAsync(id);
            return product;
        }
    }
}
