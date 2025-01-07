using Microsoft.AspNetCore.Mvc;
using ProductApi.Domain.Interfaces.Products;
using ProductApi.Domain.Models.Products;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("popular")]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetPopularProducts();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            await _productService.Add(product);
            return Ok();
        }
    }
}
