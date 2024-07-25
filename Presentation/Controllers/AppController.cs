using Domain.Repositories.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.Extensions;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppController(IGPTService gPTService) : ControllerBase
{
    [HttpGet("gpt")]
    public async Task<IActionResult> Get(string input)
    => Ok(await gPTService.ChatCompletion(input).ToResponseAsync(message: ResponseMessages.USER_ADDED));
}
