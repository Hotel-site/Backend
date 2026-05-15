using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/user")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private IUserActions _userActions;

        public UserController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _userActions = bl.GetUserActions();
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _userActions.GetAllUsersAction();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userActions.GetUserByIdAction(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found!" });
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserRegDTO user)
        {
            var NewUser = _userActions.ResponseUserCreateAction(user);
            if (NewUser.IsSuccess == false)
            {
                return BadRequest(NewUser);
            }
            return Ok(NewUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO user)
        {
            user.Id = id;
            var UpdateUser = _userActions.ResponseUserUpdateAction(user);
            if (UpdateUser.IsSuccess == false)
            {
                return BadRequest(UpdateUser);
            }
            return Ok(UpdateUser);
        }

        [HttpPut("activate/{id}")]
        public IActionResult ActivateUser(int id, [FromBody] UserActivateDTO user)
        {
            user.Id = id;
            var UpdateUser = _userActions.ResponseUserActivateAction(user);
            if (UpdateUser.IsSuccess == false)
            {
                return BadRequest(UpdateUser);
            }
            return Ok(UpdateUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userActions.ResponseUserDeleteAction(id);
            if (user.IsSuccess == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }
    }
}