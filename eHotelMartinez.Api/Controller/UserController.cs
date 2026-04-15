using eHotelMartinez.Api.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static List<User> _users = new();
        public static int _nextId = 1;


        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            return Ok(_users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} Not Found!" });
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if(user.Username == null || user.Username == "")
            {
                return BadRequest(new { Message = "Username is empty!" });
            }
            user.Id = _nextId++;
            user.CreatedAt = DateTime.UtcNow;

            _users.Add(user);

            return Created($"/api/users/{user.Id}", user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var existUser = _users.FirstOrDefault(u => u.Id == id);

            if (existUser == null)
            {
                return NotFound(new { Message = $"User with ID {id} Not Found!" });
            }

            existUser.Username = updatedUser.Username;
            existUser.Email = updatedUser.Email;
            return Ok(existUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} Not Found!" });
            }

            _users.Remove(user);
            return NoContent();
        }


    }
}