using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserActions _userActions;

        public UserController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _userActions = bl.GetUserActions();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userActions.GetAllUsersAction();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userActions.GetUserByIdAction(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found!" });
            }
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRegDTO user)
        {
            var NewUser = await _userActions.ResponseUserCreateAction(user);
            if (NewUser.IsSuccess == false)
            {
                return BadRequest(NewUser);
            }
            return Ok(NewUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDTO user)
        {
            user.Id = id;
            var UpdateUser = await _userActions.ResponseUserUpdateAction(user);
            if (UpdateUser.IsSuccess == false)
            {
                return BadRequest(UpdateUser);
            }
            return Ok(UpdateUser);
        }

        [AllowAnonymous]
        [HttpPut("password/recovery/{id}")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDTO user)
        {
            var UpdateUser = await _userActions.ResponseUserUpdatePasswordAction(user);
            if (UpdateUser.IsSuccess == false)
            {
                return BadRequest(UpdateUser);
            }
            return Ok(UpdateUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("activate/{id}")]
        public async Task<IActionResult> ActivateUser(int id, [FromBody] UserActivateDTO user)
        {
            user.Id = id;
            var UpdateUser = await _userActions.ResponseUserActivateAction(user);
            if (UpdateUser.IsSuccess == false)
            {
                return BadRequest(UpdateUser);
            }
            return Ok(UpdateUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userActions.ResponseUserDeleteAction(id);
            if (user.IsSuccess == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }
    }
}