namespace GeoPet.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using GeoPet.Entities;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // authorization
        var item = context.HttpContext.Items["PetCarer"];
        var petCarer = item as PetCarer;
        if (petCarer == null)
            context.Result = new JsonResult(new { error = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
}