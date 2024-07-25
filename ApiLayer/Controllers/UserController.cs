using Microsoft.AspNetCore.Mvc;
using Ticketing.businessLogicLayer.Services.Interfaces;
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
    public async Task<IActionResult> CreateUser(CreateUpdateUserInputDto createUpdateUserInputDto)
    {
        return Ok(await _userService.CreateUser(createUpdateUserInputDto));
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
    public async Task<IActionResult> UpdateUser(int id,  CreateUpdateUserInputDto createUpdateUserInputDto)
    {
        return Ok(await _userService.UpdateUser(createUpdateUserInputDto, id));
    }
}