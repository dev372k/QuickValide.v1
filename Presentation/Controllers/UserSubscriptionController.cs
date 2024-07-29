using Domain.Entities;
using Domain.IRepositories;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.DTOs.UserSubscriptionDTOs;
using Shared.Extensions;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserSubscriptionController(IUserSubscriptionRepo _userSubscriptionRepo) : ControllerBase
{
    [HttpPost, Authorize]
    public async Task<IActionResult> Post(AddUserSubscriptionDTO request)
        => Ok(await _userSubscriptionRepo.AddAsync(request).ToResponseAsync(message: ResponseMessages.SUBSCRIPTION_ADDED));

    [HttpPut("{id}"), Authorize]
    public async Task<IActionResult> Put(int id,UpdateUserSubscriptionDTO request)
       => Ok(await _userSubscriptionRepo.UpdateAsync(id,request).ToResponseAsync(message: ResponseMessages.SUBSCRIPTION_UPDATED));

    [HttpPatch("{id}"), Authorize]
    public async Task<IActionResult> Patch(int id, bool status)
      => Ok(await _userSubscriptionRepo.UpdateStatusAsync(id, status).ToResponseAsync(message: ResponseMessages.SUBSCRIPTION_UPDATED));

    [HttpGet("{userid}"), Authorize]
    public async Task<IActionResult> Get(int userid)
        => Ok(await _userSubscriptionRepo.GetAsync(userid).ToResponseAsync());

    [HttpDelete("{id}"), Authorize]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _userSubscriptionRepo.DeleteAsync(id).ToResponseAsync(message: ResponseMessages.SUBSCRIPTION_DELETED));
}
