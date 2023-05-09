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
                new Product() {ProductName = "chocolatte", Price=13},
                new Product() {ProductName = "bread", Price=13},
                new Product() {ProductName = "chips", Price=13},
                new Product() {ProductName = "patato", Price=13}
            };
            return products;
        }
    }
}
