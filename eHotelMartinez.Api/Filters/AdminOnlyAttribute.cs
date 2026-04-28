using eHotelMartinez.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eHotelMartinez.Api.Filters
{
    public class AdminOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Items.TryGetValue("Role", out var roleObj) || roleObj is not UserRole role)
            {
                context.Result = new UnauthorizedObjectResult(new { IsSuccess = false, message = "Unauthorized" });
                return;
            }

            if (role != UserRole.Admin)
            {
                context.Result = new ObjectResult(new { IsSuccess = false, message = "Forbidden" })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
                return;
            }
        }
    }
}