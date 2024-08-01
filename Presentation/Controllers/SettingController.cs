using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.DTOs.SettingDTOs;
using Shared.Extensions;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController(SettingRepo _settingRepo) : ControllerBase
    {
        [HttpPut("{appId:int}"), Authorize]
        [IsAuthorized(["Admin", "User"])]
        public async Task<IActionResult> Put(int appId, UpdateSettingDTO request)
            => Ok(await _settingRepo.UpdateAsync(appId, request).ToResponseAsync(message: ResponseMessages.APP_UPDATED));

        [HttpGet("{id}")]
        [IsAuthorized(["Admin", "User"])]
        public async Task<IActionResult> Get(int id)
            => Ok(await _settingRepo.GetAsync(id).ToResponseAsync());

        [HttpPut("{appId:int}/{IsLive:bool}"), Authorize]
        [IsAuthorized(["Admin", "User"])]
        public async Task<IActionResult> Put(int appId, bool IsLive)
            => Ok(await _settingRepo.UpdateAppStatusAsync(appId, IsLive).ToResponseAsync(message: ResponseMessages.APP_UPDATED));
    }
}
