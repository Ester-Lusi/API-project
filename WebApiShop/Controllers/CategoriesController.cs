using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _icategoryService;
        public CategoriesController(ICategoryService icategoryService)
        {
            _icategoryService = icategoryService;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<List<Category>> Get()
        {
            return await _icategoryService.GetCategory();
        }
    }
}
