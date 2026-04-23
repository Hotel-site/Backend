using eHotelMartinez.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eHotelMartinez.Api.Filters
{
    public class AdminOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(!context.HttpContext.Items.TryGetValue("Role", out var roleobj) || roleobj is not UserRole role)
            {
                context.Result = new UnauthorizedObjectResult(new { IsSuccess=false, message = "Unauthorized" });
                return;
            }

            if (role != UserRole.Admin)
            {
                context.Result = new ForbidResult();
                return;
            }
        }

    }
}
