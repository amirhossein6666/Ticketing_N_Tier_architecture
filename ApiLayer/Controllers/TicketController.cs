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
}