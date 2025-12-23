using Dtos;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(string? name,
            int[]? categories, int? nimPrice, int? maxPrice, int? limit,
            string? orderBy, int? offset)
        {
            IEnumerable<ProductDto> products = await _productService.GetProducts(name, categories, nimPrice, maxPrice, limit, orderBy, offset);
            if (products == null)
                return NoContent();
            return Ok(products);
        }

    }
}
