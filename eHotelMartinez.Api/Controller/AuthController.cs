using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserActions _userActions;

        public AuthController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _userActions = bl.GetUserActions();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserAuthDTO auth)
        {
            var AuthResult = _userActions.ResponseUserLoginAction(auth);

            if(!AuthResult.IsSuccess)
            {
                return Unauthorized(AuthResult.Message);
            }

            return Ok(new { token = AuthResult.Message });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegDTO user)
        {
            var RegUser = _userActions.ResponseUserCreateAction(user);

            if (RegUser.IsSuccess == false)
            {
                return BadRequest(RegUser);
            }
            return Ok(RegUser);
        }
    }
}
