using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eHotelMartinez.Api.Controller
{
    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
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
        public async Task<IActionResult> Login([FromBody] UserAuthDTO auth)
        {
            var AuthResult = await _userActions.ResponseUserLoginAction(auth);

            if(!AuthResult.IsSuccess)
            {
                return await Task.FromResult(Unauthorized(AuthResult.Message));
            }

            return await Task.FromResult(Ok(new { token = AuthResult.Message }));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegDTO user)
        {
            var RegUser = await _userActions.ResponseUserCreateAction(user);

            if (RegUser.IsSuccess == false)
            {
                return await Task.FromResult(BadRequest(RegUser));
            }
            return await Task.FromResult(Ok(RegUser));
        }
    }
}
