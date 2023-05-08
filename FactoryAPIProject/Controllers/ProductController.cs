using FactoryAPIProject.Models;
using FactoryAPIProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        [HttpGet]
        [Route("GetProduct")]
        public async Task<List<Product>> GetProduct()
        {
            return await _productService.GetAllProductAsync();
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody]ProductModel product)
        {
            await _productService.AddProductAsync(product);
            return StatusCode(StatusCodes.Status200OK,new Response { Status = "Success !", Message = "Product added succesfully !", isSuccess = true });
        }
    }
}
