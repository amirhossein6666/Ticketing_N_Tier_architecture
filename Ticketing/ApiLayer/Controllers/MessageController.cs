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

    [HttpPost]
    public async Task<IActionResult> CreateMessage(MessageInputDto messageInputDto)
    {
        messageInputDto.SenderId = GetUserInfo();
        return Ok(await _messageService.CreateMessage(messageInputDto));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMessageById(int id)
    {
        return Ok(await _messageService.GetMessageById(id));
    }

    [HttpGet("GetMessagesByTicketId/{ticketId:int}")]
    public async Task<IActionResult> GetMessagesByTicketId(int ticketId)
    {
        return Ok(await _messageService.GetMessagesByTicketId(ticketId));
    }

    [HttpGet("GetMessagesByUserId/{userId:int}")]
    public async Task<IActionResult> GetMessagesByUserId(int userId)
    {
        return Ok(await _messageService.GetMessagesByUserId(userId));
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateMessage(int id, UpdateMessageDto updateMessageDto)
    {
        return Ok(await _messageService.UpdateMessage(id, updateMessageDto));
    }

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