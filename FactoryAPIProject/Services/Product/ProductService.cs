using FactoryAPIProject.Data;
using Microsoft.EntityFrameworkCore;

namespace FactoryAPIProject.Services.Product
{
    public class ProductService : IProductService
    {
        public ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Models.Product>> GetAllProductAsync()
        {
            if (!_context.Products.Any())
            {
                await _context.Products.AddRangeAsync(SeedProductData());
                await _context.SaveChangesAsync();
            }
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public List<Models.Product> SeedProductData()
        {
            List<Models.Product> products = new List<Models.Product>()
            {
                new Models.Product() { ProductName = "erik", Price=12.3f},
                new Models.Product() { ProductName = "apple", Price=14.6f},
                new Models.Product() { ProductName = "orange", Price=11.9f},
                new Models.Product() { ProductName = "grape", Price=13.8f},
                new Models.Product() { ProductName = "cake", Price=13.8f},
                new Models.Product() { ProductName = "chocolatte", Price=13.8f},
                new Models.Product() { ProductName = "bread", Price=13.8f},
                new Models.Product() { ProductName = "chips", Price=13.8f},
                new Models.Product() { ProductName = "patato", Price=13.8f}
            };
            return products;
        }
    }
}
