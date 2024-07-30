using Application.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.DTOs.AppDTOs;
using Shared.Extensions;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppController(AppRepo _appRepo) : ControllerBase
{
    [HttpPost, Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Post(AddAppDTO request)
        => Ok(await _appRepo.AddAsync(request).ToResponseAsync(message: ResponseMessages.APP_ADDED));

    [HttpPut, Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Put(UpdateAddAppDTO request)
       => Ok(await _appRepo.UpdateAsync(request).ToResponseAsync(message: ResponseMessages.APP_UPDATED));

    [HttpGet, Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Get()
        => Ok(await _appRepo.GetAsync().ToResponseAsync());

    [HttpGet("{id}"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Get(int id)
        => Ok(await _appRepo.GetAsync(id).ToResponseAsync());

    [HttpDelete("{id}"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _appRepo.DeleteAsync(id).ToResponseAsync(message: ResponseMessages.USER_DELETED));
}
