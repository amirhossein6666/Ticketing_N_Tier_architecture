using AutoMapper;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Enums;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.MessageDtos;
using Ticketing.Dtos.ResponseDtos.MessageResponseDtos;
using Ticketing.Dtos.ResponseDtos.TicketResponseDtos;
using Ticketing.Dtos.TicketDtos;

namespace Ticketing.businessLogicLayer.Services.Implementations;

public class TicketService: ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public TicketService(ITicketRepository ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task<CreateUpdateTicketResponseDto> CreateTicket(TicketInputDto ticketInputDto)
    {
        var ticket = _mapper.Map<Ticket>(ticketInputDto);
        ticket.Status = Status.Unread;
        try
        {
            var returnedTicket = await _ticketRepository.CreateTicket(ticket);
            return new CreateUpdateTicketResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status201Created,
                Message = $"ticket with id {returnedTicket.Id} created",
                Data = _mapper.Map<CreateUpdateTicketDto>(returnedTicket)
            };
        }
        catch (Exception e)
        {
            return new CreateUpdateTicketResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.ToString()
            };
        }

    }

    public async Task<TicketResponseDto> GetTicketById(int id)
    {
        var ticket = await _ticketRepository.GetTicketById(id);
        if (ticket is null)
        {
            return new TicketResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = "ticket Not Found"
            };
        }
        return new TicketResponseDto()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Ok",
            Data = _mapper.Map<TicketDto>(ticket)
        };
    }
}