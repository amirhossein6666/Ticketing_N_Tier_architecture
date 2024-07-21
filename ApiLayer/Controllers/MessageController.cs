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
        return Ok(await _messageService.CreateMessage(messageInputDto));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMessageById(int id)
    {
        return Ok(await _messageService.GetMessageById(id));
    }
}