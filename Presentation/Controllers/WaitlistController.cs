﻿using Application.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.DTOs.WaitlistDTOs;
using Shared.Extensions;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WaitlistController(WaitlistRepo _waitlistRepo) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(AddWaitlistDTO request)
        => Ok(await _waitlistRepo.AddAsync(request).ToResponseAsync(message: ResponseMessages.WAITLIST_ADDED));

    [HttpGet("{appid}"),Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Get(int appid)
        => Ok(await _waitlistRepo.GetAsync(appid).ToResponseAsync());

}
