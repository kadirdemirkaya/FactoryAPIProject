using FactoryAPIProject.Models;
using FactoryAPIProject.Services.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAPIProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        //31
        [HttpGet]
        public async Task<List<Product>> GetProduct()
        {
            return await _productService.GetAllProductAsync();
        }
    }
}
