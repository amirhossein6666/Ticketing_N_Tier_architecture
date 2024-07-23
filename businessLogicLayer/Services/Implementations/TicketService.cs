using AutoMapper;
using Ticketing.businessLogicLayer.Services.Interfaces;

namespace Ticketing.businessLogicLayer.Services.Implementations;

public class TicketService: ITicketService
{
    private readonly ITicketService _ticketService;
    private readonly IMapper _mapper;

    public TicketService(ITicketService ticketService, IMapper mapper)
    {
        _ticketService = ticketService;
        _mapper = mapper;
    }
}