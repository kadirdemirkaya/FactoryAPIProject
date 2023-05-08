using FactoryAPIProject.Data;
using FactoryAPIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FactoryAPIProject.Services
{
    public class ProductService : IProductService
    {
        public ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(ProductModel product)
        {
            var prod = new Product()
            {
                ProductName = product.ProductName,
                Price = product.Price
            };
            await _context.Products.AddAsync(prod);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            if (!_context.Products.Any())
            {
                await _context.Products.AddRangeAsync(SeedProductData());
                await _context.SaveChangesAsync();
            }
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public List<Product> SeedProductData()
        {
            List<Models.Product> products = new List<Models.Product>()
            {
                new Models.Product() {ProductId = 1, ProductName = "erik", Price=12.3f},
                new Models.Product() {ProductId = 2, ProductName = "apple", Price=14.6f},
                new Models.Product() {ProductId = 3, ProductName = "orange", Price=11.9f},
                new Models.Product() {ProductId = 4, ProductName = "grape", Price=13.8f},
                new Models.Product() {ProductId = 5, ProductName = "cake", Price=13.8f},
                new Models.Product() {ProductId = 6, ProductName = "chocolatte", Price=13.8f},
                new Models.Product() {ProductId = 7, ProductName = "bread", Price=13.8f},
                new Models.Product() {ProductId = 8, ProductName = "chips", Price=13.8f},
                new Models.Product() {ProductId = 9, ProductName = "patato", Price=13.8f}
            };
            return products;
        }
    }
}
