using Application.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Commons;
using Shared.DTOs.UserDTOs;
using Shared.Extensions;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(UserRepo _userRepo) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(AddUserDTO request)
        => Ok(await _userRepo.AddAsync(request).ToResponseAsync(message: ResponseMessages.USER_ADDED));

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDTO request)
        => Ok(await _userRepo.LoginAsync(request).ToResponseAsync());

    [HttpPost("google")]
    public async Task<IActionResult> Google(GoogleLoginDTO request)
        => Ok(await _userRepo.GoogleLoginAsync(request).ToResponseAsync());

    [HttpPost("forgot-password/{email}")]
    public async Task<IActionResult> ForgetPassword(string email)
        => Ok(await _userRepo.UpdatePasswordAsync(email).ToResponseAsync(message: ResponseMessages.NEW_PASSWORD_SENT));

    [HttpGet, Authorize]
    [IsAuthorized(["Admin"])]
    public async Task<IActionResult> Get()
        => Ok(await _userRepo.GetAsync().ToResponseAsync());

    [HttpGet("{id}"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Get(int id)
        => Ok(await _userRepo.GetAsync(id).ToResponseAsync());

    [HttpPut("{id}"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Put(int id, UpdateUserDTO request)
        => Ok(await _userRepo.UpdateAsync(id, request).ToResponseAsync(message: ResponseMessages.USER_UPDATED));

    [HttpDelete("{id}"), Authorize]
    [IsAuthorized(["Admin", "User"])]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _userRepo.DeleteAsync(id).ToResponseAsync(message: ResponseMessages.USER_DELETED));
}
