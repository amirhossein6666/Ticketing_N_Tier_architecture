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
    [HttpPost]
    public async Task<IActionResult> CreateTicket(TicketInputDto ticketInputDto)
    {
        return Ok(await _ticketService.CreateTicket(ticketInputDto));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTicketById(int id)
    {
        return Ok(await _ticketService.GetTicketById(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        return Ok(await _ticketService.GetAllTickets());
    }
    [HttpGet("GetTicketsByCreatorId/creatorId")]
    public async Task<IActionResult> GetTicketsByCreatorId(int creatorId)
    {
        return Ok(await _ticketService.GetTicketsByCreatorId(creatorId));
    }
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateTicket(int id, UpdateTicketInputDto updateTicketInputDto)
    {
        return Ok(await _ticketService.UpdateTicket(id, updateTicketInputDto));
    }

    [HttpPost("SetRating/{id:int}")]
    public async Task<IActionResult> SetTicketRating(int id, int rating)
    {
        return Ok(await _ticketService.SetTicketRating(id, rating));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        return Ok(await _ticketService.DeleteTicket(id));
    }

    [HttpPost("/FinishTicket")]
    public async Task<IActionResult> FinishTicket(FinishTicketInputDto finishTicketInputDto)
    {
        return Ok(await _ticketService.FinishTicket(finishTicketInputDto));
    }
}