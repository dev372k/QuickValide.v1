using Domain.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.DTOs.AppDTOs;
using Shared.Extensions;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppController(IAppRepo _appRepo) : ControllerBase
{
    [HttpPost("Add")]
    public async Task<IActionResult> Add(AddAppDTO request)
        => Ok(await _appRepo.AddAsync(request).ToResponseAsync(message: ResponseMessages.APP_ADDED));

    [HttpPost("Update")]
    public async Task<IActionResult> Update(UpdateAddAppDTO request)
       => Ok(await _appRepo.UpdateAsync(request).ToResponseAsync(message: ResponseMessages.APP_UPDATED));

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _appRepo.GetAllAsync().ToResponseAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
        => Ok(await _appRepo.GetByIdAsync(id).ToResponseAsync());

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _appRepo.DeleteAsync(id).ToResponseAsync(message: ResponseMessages.USER_DELETED));
}
