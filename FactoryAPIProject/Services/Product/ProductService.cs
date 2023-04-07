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
            var products = await _context.Products.ToListAsync();
            return products;
        }
    }
}
