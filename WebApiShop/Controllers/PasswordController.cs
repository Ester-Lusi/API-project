using Microsoft.AspNetCore.Mvc;
using Entities;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController: ControllerBase
    {
        private IPasswordService _passwordService;
        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpPost]
        public ActionResult<Password> Post([FromBody] string password)
        {
            Password pass = _passwordService.CheckStrength(password);
            if (pass == null)
                return NoContent();
            return Ok(pass);
        }
    }
}
