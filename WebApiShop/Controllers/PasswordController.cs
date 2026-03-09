using Microsoft.AspNetCore.Mvc;
using Entities;
using Services;
using Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController: ControllerBase
    {
        private readonly IPasswordService _passwordService;
        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpPost]
        public ActionResult<int> PasswordScore([FromBody] string password)
        {
            int strength = _passwordService.GetPasswordStrength(password);
            if (strength == null)
                  return NoContent();
            return Ok(strength);
        }
    }
}
