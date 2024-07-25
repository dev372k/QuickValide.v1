using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using System.Security.Claims;
using Shared.DTOs.UserDTOs;

public class IsAuthorizedAttribute : Attribute, IActionFilter
{
    private string[] _role;

    public IsAuthorizedAttribute(string[] role)
    {
        _role = role;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        string jwtToken = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(jwtToken))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            return;
        }

        if (jwtToken.ToLower().StartsWith("bearer "))
            jwtToken = jwtToken.Substring(7).Trim();
        else
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            return;
        }

        GetUserDTO userData = ValidateJWTAndGetUserData(jwtToken);

        if (userData != null && UserHasPermission(userData, _role))
            return;

        context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
    }

    public void OnActionExecuted(ActionExecutedContext context) { }

    private GetUserDTO ValidateJWTAndGetUserData(string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);

        var userDataJson = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.UserData)?.Value;
        if (!string.IsNullOrEmpty(userDataJson))
            return JsonConvert.DeserializeObject<GetUserDTO>(userDataJson)!;
        return new GetUserDTO();
    }

    private bool UserHasPermission(GetUserDTO userData, string[] role)
    {
        return role.Contains(userData.Role);
    }
}

