using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.DataAccessLayer.Enums;
using Ticketing.Dtos.UserDtos;

namespace Ticketing.ApiLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserInputDto createUserInputDto)
    {
        return Ok(await _userService.CreateUser(createUserInputDto));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        return Ok(await _userService.GetUserById(id));
    }

    [HttpGet("GetUserByUsername/username")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        return Ok(await _userService.GetUserByUsername(username));
    }
    [HttpGet("GetUsersByRole/role")]
    public async Task<IActionResult> GetUsersByRole(string role)
    {
        return Ok(await _userService.GetUsersByRole(role));
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id,  UpdateUserInputDto updateUserInputDto)
    {
        return Ok(await _userService.UpdateUser(updateUserInputDto, id));
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        return Ok(await _userService.DeleteUser(id));
    }
    [HttpPost("/UserSetRating")]
    public async Task<IActionResult> UserSetRating(UserSetRatingInputDto userSetRatingInputDto)
    {
        userSetRatingInputDto.RatedUserId = GetUserInfo();
        return Ok(await _userService.UserSetRating(userSetRatingInputDto));
    }
    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginInputDto loginInputDto)
    {
        return Ok(await _userService.Login(loginInputDto));
    }

    private int GetUserInfo()
    {
        var userIdClaim = User.FindFirst("userId")?.Value;

        var userId = int.Parse(userIdClaim);

        return userId;
    }
}