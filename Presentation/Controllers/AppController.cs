﻿using Application.Implementations;
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

    [HttpPut("{id:int}"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Put(int id, UpdateAppDTO request)
        => Ok(await _appRepo.UpdateAsync(id, request).ToResponseAsync(message: ResponseMessages.APP_UPDATED));

    [HttpGet, Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Get()
        => Ok(await _appRepo.GetAsync().ToResponseAsync());

    [HttpGet("{id}"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Get(int id)
        => Ok(await _appRepo.GetAsync(id).ToResponseAsync());

    [HttpGet("{name}/GetByName")]
    public async Task<IActionResult> Get(string name)
    => Ok(await _appRepo.GetAsync(name).ToResponseAsync());

    [HttpDelete("{id}"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _appRepo.DeleteAsync(id).ToResponseAsync(message: ResponseMessages.APP_UPDATED));

    [HttpPatch("{id}/analytics"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Analytics(int id, UpdateAnalyticsDTO request)
        => Ok(await _appRepo.UpdateGoogleURLAsync(id, request.URL).ToResponseAsync(message: ResponseMessages.APP_UPDATED));

    [HttpGet("{id}/analytics"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Analytics(int id)
        => Ok(await _appRepo.GetGoogleURLAsync(id).ToResponseAsync());
    
    [HttpPatch("{id}/seo"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> SEO(int id, UpdateSEODTO request)
        => Ok(await _appRepo.UpdateSEOAsync(id, request).ToResponseAsync(message: ResponseMessages.APP_UPDATED));

    [HttpGet("{id}/seo"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> SEO(int id)
        => Ok(await _appRepo.GetSEOAsync(id).ToResponseAsync());
}
