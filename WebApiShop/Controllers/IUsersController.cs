using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface IUsersController
    {
        void DeleteUser(int id);
        Task<ActionResult<User>> GetById(int id);
        Task<ActionResult<User>> Login([FromBody] User user);
        Task<ActionResult<User>> Post([FromBody] User user);
        Task<ActionResult> UpdateUser(int id, [FromBody] User user);
        Task<ActionResult<IEnumerable<User>>> Get();
    }
}