using Domain.Repositories.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.Extensions;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppController(IGPTService gPTService, IPaymentGateway paymentGateway) : ControllerBase
{
    [HttpGet("gpt")]
    public async Task<IActionResult> Get(string input)
        => Ok(await gPTService.ChatCompletion(input).ToResponseAsync(message: ResponseMessages.USER_ADDED));

    [HttpGet("subscription")]
    public async Task<IActionResult> SubscriptionGet()
        => Ok(await paymentGateway.Get().ToResponseAsync());
    
    [HttpGet("subscription/{subscriptionId}")]
    public async Task<IActionResult> SubscriptionGet(string subscriptionId)
        => Ok(await paymentGateway.Get(subscriptionId).ToResponseAsync());
    
    [HttpPost("subscription")]
    public async Task<IActionResult> SubscriptionPost()
        => Ok(await paymentGateway.Create("cus_QXXBRx59zPUbfS", "price_1PgSBwEuB9xz5lDpC7VxA5yI").ToResponseAsync());
}
