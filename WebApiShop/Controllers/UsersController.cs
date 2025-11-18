using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase, IUsersController
    {
        UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _userService.Get();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]

        public ActionResult<User> GetById(int id)
        {
            if (_userService.GetById(id) == null)
            {
                return NoContent();
            }
            return Ok(_userService.GetById(id));
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            User userResult = _userService.AddUser(user);
            if (userResult == null)
            {
                return BadRequest("אחד השדות ריקים או סיסמא חלשה");
            }
            return CreatedAtAction(nameof(Get), new { user.Id }, user);
        }

        [HttpPost("Login")]
        public ActionResult<User> Login([FromBody] User user)
        {
            User userResult = _userService.FindUser(user);
            if (userResult == null)
                return NotFound();
            return CreatedAtAction(nameof(Get), new { userResult.Id }, userResult);

        }


        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            var result = _userService.UpdateUser(id, user);
            if (result < 2)
                return BadRequest("רישום נכשל - סיסמא חלשה");
            return Ok(user);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void DeleteUser(int id)
        {
        }
    }
}
