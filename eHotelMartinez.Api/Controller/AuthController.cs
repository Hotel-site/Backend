using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.DataAccess.Context;
using eHotelMartinez.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eHotelMartinez.Api.Filters;

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
        public IActionResult Login([FromBody] UserAuthDTO auth)
        {
            var AuthResult = _userActions.ResponseUserLoginAction(auth);

            if (AuthResult.IsSuccess == false)
            {
                return BadRequest(AuthResult);
            }
            using (var db = new UserContext())
            {
                var email = auth.Email.ToLower();
                var user = db.Users.FirstOrDefault(u => u.Email == email && u.IsActive);

                if (user == null)
                {
                    return BadRequest(new { IsSuccess = false, message = "User not found!" });
                }
                var sessionKey = _sessionActions.CreateOrUpdateSession(user.Id);

                Response.Cookies.Append("X-KEY", sessionKey, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddMinutes(60)
                });
            }
            return Ok(AuthResult);
        }

        [SessionAuthFilter]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var cookie = Request.Cookies["X-KEY"];

            if (!string.IsNullOrWhiteSpace(cookie))
            {
                _sessionActions.DeleteSession(cookie);
            }

            Response.Cookies.Append("X-KEY", "", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddDays(-1)
            });
            return Ok(new { IsSuccess = true, message = "Logged out Successfully" });
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
