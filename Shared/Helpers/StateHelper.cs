using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shared.DTOs.UserDTOs;
using System.Security.Claims;

namespace Shared;

public interface IStateHelper
{
    GetUserDTO User();
}
public class StateHelper : IStateHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public StateHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public GetUserDTO User()
    {
        var result = string.Empty;
        if (_httpContextAccessor.HttpContext is not null)      
            result = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.UserData).Value;
       
        return JsonConvert.DeserializeObject<GetUserDTO>(result!)!;
    }
}