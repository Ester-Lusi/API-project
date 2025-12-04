using Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApiShop.Controllers
{
    public interface IPasswordController
    {
        //void Delete(int id);
        //IEnumerable<string> Get();
        //string Get(int id);
        ActionResult<Password> Post([FromBody] string password);
        //void Put(int id, [FromBody] string value);
    }
}