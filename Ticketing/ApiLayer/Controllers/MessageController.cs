using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.Dtos.MessageDtos;

namespace Ticketing.ApiLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController: ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateMessage(MessageInputDto messageInputDto)
    {
        messageInputDto.SenderId = GetUserInfo();
        return Ok(await _messageService.CreateMessage(messageInputDto));
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMessageById(int id)
    {
        return Ok(await _messageService.GetMessageById(id));
    }

    [Authorize]
    [HttpGet("GetMessagesByTicketId/{ticketId:int}")]
    public async Task<IActionResult> GetMessagesByTicketId(int ticketId)
    {
        return Ok(await _messageService.GetMessagesByTicketId(ticketId));
    }

    [Authorize]
    [HttpGet("GetMessagesByUserId/{userId:int}")]
    public async Task<IActionResult> GetMessagesByUserId(int userId)
    {
        return Ok(await _messageService.GetMessagesByUserId(userId));
    }

    [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateMessage(int id, UpdateMessageDto updateMessageDto)
    {
        return Ok(await _messageService.UpdateMessage(id, updateMessageDto));
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleleMessage(int id)
    {
        return Ok(await _messageService.DeleteMessage(id));
    }

    private int GetUserInfo()
    {
        var userIdClaim = User.FindFirst("userId")?.Value;

        var userId = int.Parse(userIdClaim);

        return userId;
    }

}