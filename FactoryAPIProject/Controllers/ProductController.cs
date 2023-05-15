using FactoryAPIProject.Models;
using FactoryAPIProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetAllProduct")]
        public async Task<List<Product>> GetAllProduct()
        {
            return await _productService.GetAllProductAsync();
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            await _productService.AddProductAsync(product);
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success !", Message = "Product added succesfully !", isSuccess = true });
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromRoute]int id)
        {
            await _productService.DeleteProductAsync(id);
            return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success !", Message = "Product delete succesfully !", isSuccess = true });
        }
    }
}
