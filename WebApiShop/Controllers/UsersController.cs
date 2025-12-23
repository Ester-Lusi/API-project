using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;
using Dtos;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            IEnumerable<UserDto> users = await _userService.GetUsers();
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            UserDto user = await _userService.GetById(id);
            if (user == null)
                return NoContent();
            return Ok(user);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody] User user)
        {
            UserDto userResult = await _userService.AddUser(user);
            if (userResult == null)
                return BadRequest("סיסמא חלשה");
            return CreatedAtAction(nameof(Get), new { user.Id }, user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            User userResult = await _userService.FindUser(user);
            if (userResult == null)
                return Unauthorized();
            return Ok(userResult);
        }


        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
        {
            var result = await _userService.UpdateUser(id, user);
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
