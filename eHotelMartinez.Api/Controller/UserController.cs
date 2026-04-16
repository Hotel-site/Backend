using eHotelMartinez.Domain.Models.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


//namespace eHotelMartinez.Api.Controller
//{
//    [Route("api/user")]
//    [ApiController]
//    public class UserDTOController : ControllerBase
//    {
//        public static List<UserDTO> _users = new();
//        public static int _nextId = 1;


//        [HttpGet("all")]
//        public IActionResult GetAllUserDTOs()
//        {
//            return Ok(_users);
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetUserDTOById(int id)
//        {
//            var user = _users.FirstOrDefault(u => u.Id == id);
//            if (user == null)
//            {
//                return NotFound(new { Message = $"UserDTO with ID {id} Not Found!" });
//            }
//            return Ok(user);
//        }

//        [HttpPost]
//        public IActionResult CreateUserDTO([FromBody] UserDTO user)
//        {
//            if(user.UserDTOname == null || user.UserDTOname == "")
//            {
//                return BadRequest(new { Message = "UserDTOname is empty!" });
//            }
//            user.Id = _nextId++;
//            user.CreatedAt = DateTime.UtcNow;

//            _users.Add(user);

//            return Created($"/api/users/{user.Id}", user);
//        }

//        [HttpPut("{id}")]
//        public IActionResult UpdateUserDTO(int id, [FromBody] UserDTO updatedUserDTO)
//        {
//            var existUserDTO = _users.FirstOrDefault(u => u.Id == id);

//            if (existUserDTO == null)
//            {
//                return NotFound(new { Message = $"UserDTO with ID {id} Not Found!" });
//            }

//            existUserDTO.UserDTOname = updatedUserDTO.UserDTOname;
//            existUserDTO.Email = updatedUserDTO.Email;
//            return Ok(existUserDTO);
//        }

//        [HttpDelete("{id}")]
//        public IActionResult DeleteUserDTO(int id)
//        {
//            var user = _users.FirstOrDefault(u => u.Id == id);

//            if (user == null)
//            {
//                return NotFound(new { Message = $"UserDTO with ID {id} Not Found!" });
//            }

//            _users.Remove(user);
//            return NoContent();
//        }


//    }
//}