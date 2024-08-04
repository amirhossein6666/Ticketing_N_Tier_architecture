using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.Dtos.TicketDtos;

namespace Ticketing.ApiLayer.Controllers;
[ApiController]
[Route("[controller]")]
public class TicketController: ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateTicket(TicketInputDto ticketInputDto)
    {
        ticketInputDto.CreatorId = GetUserInfo().Value;
        return Ok(await _ticketService.CreateTicket(ticketInputDto));
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTicketById(int id)
    {
        return Ok(await _ticketService.GetTicketById(id));
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        return Ok(await _ticketService.GetAllTickets());
    }

    [Authorize]
    [HttpGet("GetTicketsByCreatorId/creatorId")]
    public async Task<IActionResult> GetTicketsByCreatorId(int creatorId)
    {
        return Ok(await _ticketService.GetTicketsByCreatorId(creatorId));
    }

    [Authorize]
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateTicket(int id, UpdateTicketInputDto updateTicketInputDto)
    {
        return Ok(await _ticketService.UpdateTicket(id, updateTicketInputDto));
    }

    [Authorize]
    [HttpPost("SetRating/{id:int}")]
    public async Task<IActionResult> SetTicketRating(int id, int rating)
    {
        var creatorId = GetUserInfo().Value;
        return Ok(await _ticketService.SetTicketRating(creatorId, id , rating));
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        return Ok(await _ticketService.DeleteTicket(id));
    }
    [Authorize]
    [HttpPost("FinishTicket")]
    public async Task<IActionResult> FinishTicket(FinishTicketInputDto finishTicketInputDto)
    {
        finishTicketInputDto.SubmitterId = GetUserInfo().Value;
        return Ok(await _ticketService.FinishTicket(finishTicketInputDto));
    }

    private ActionResult<int> GetUserInfo()
    {
        var userIdClaim = User.FindFirst("userId")?.Value;
        if (userIdClaim == null)
        {
            return Unauthorized();
        }
        var userId = int.Parse(userIdClaim);

        return userId;
    }
}