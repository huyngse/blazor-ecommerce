using BlazorEcommerceSharedLibrary.Contracts;
using BlazorEcommerceSharedLibrary.Models;
using BlazorEcommerceSharedLibrary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct productService;

        public ProductController(IProduct productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts([FromQuery] bool featured)
        {
            var products = await productService.GetProducts(featured);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddProduct([FromBody] Product model)
        {
            if (model == null) { return BadRequest("Model is null"); }
            var response = await productService.AddProduct(model);
            return Ok(response);
        }
    }
}
