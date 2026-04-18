using eHotelMartinez.Domain.Models.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace eHotelMartinez.Api.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static List<UserDTO> _users = new();
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
        public IActionResult CreateUser([FromBody] UserRegDTO RegUser)
        {   
            if (RegUser == null)
            {
                return BadRequest(new { Message = "Body is empty!" });
            }
            if (string.IsNullOrWhiteSpace(RegUser.Username))
            {
                return BadRequest(new { Message = "Username is empty!" });
            }
            if (string.IsNullOrWhiteSpace(RegUser.Email))
            {
                return BadRequest(new { Message = "Email is empty!" });
            }
            if (string.IsNullOrWhiteSpace(RegUser.Password))
            {
                return BadRequest(new { Message = "Password is empty!" });
            }
            if (_users.Any(u => u.Email.Equals(RegUser.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict(new { Message = " User with this Email already exist!" });
            }

            var user = new UserDTO
            {
                Id = _nextId++,
                Username = RegUser.Username.Trim(),
                Email = RegUser.Email.Trim()
            };

            _users.Add(user);
            return Created($"/api/users/{user.Id}", user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO updatedUser)
        {
            var existUser = _users.FirstOrDefault(u => u.Id == id);

            if (existUser == null)
            {
                return NotFound(new { Message = $"User with ID {id} Not Found!" });
            }


            existUser.Username = updatedUser.Username.Trim();
            existUser.Email = updatedUser.Email.Trim();
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