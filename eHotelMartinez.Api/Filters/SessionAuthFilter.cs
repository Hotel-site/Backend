using eHotelMartinez.BusinessLogic.Interfaces;
using eHotelMartinez.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eHotelMartinez.Api.Filters
{
    public class SessionAuthFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<Microsoft.AspNetCore.Authorization.IAllowAnonymous>().Any();

            if (allowAnonymous)
            {
                return;
            }

            var cookies = context.HttpContext.Request.Cookies["X-KEY"];
            if (string.IsNullOrEmpty(cookies))
            {
                context.Result = new UnauthorizedObjectResult(new { IsSuccess = false, message = "Unauthorized" });
                return;
            }

            var bl = new BusinessLogic.BusinessLogic();
            ISessionAction sessionAction = bl.GetSessionActions();

            var userId = sessionAction.GetUserIdFromSession(cookies);

            if (userId == null)
            {
                context.HttpContext.Response.Cookies.Append("X-KEY", "", new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(-1),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                context.Result = new UnauthorizedObjectResult(new { IsSuccess = false, message = "Session is Expired or Invalid" });
                return;
            }

            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId.Value && u.IsActive);
                if (user == null)
                {
                    context.Result = new UnauthorizedObjectResult(new { IsSuccess = false, message = "User not found or inactive" });
                    return;
                }

                context.HttpContext.Items["UserId"] = userId.Value;
                context.HttpContext.Items["Role"] = user.Role;
            }

        }

    }
}
