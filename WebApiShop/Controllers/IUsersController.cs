using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface IUsersController
    {
        void DeleteUser(int id);
        IEnumerable<string> Get();
        ActionResult<User> GetById(int id);
        ActionResult<User> Login([FromBody] User user);
        ActionResult<User> Post([FromBody] User user);
        IActionResult UpdateUser(int id, [FromBody] User user);
    }
}