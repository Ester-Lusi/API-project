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
        public async Task<PageResponseDto<ProductDto>> Get(string? name, [FromQuery] int?[] categories, int? minPrice, int? maxPrice, int? position, int skip, string? orderBy, string? description)
        {
            return await _productService.GetProducts(name, categories, minPrice, maxPrice, position, skip, orderBy, description);
        }

    }
}
