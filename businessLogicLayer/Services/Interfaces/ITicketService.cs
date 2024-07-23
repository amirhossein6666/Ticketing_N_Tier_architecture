using Ticketing.Dtos.ResponseDtos.TicketResponseDtos;
using Ticketing.Dtos.TicketDtos;

namespace Ticketing.businessLogicLayer.Services.Interfaces;

public interface ITicketService
{
    public Task<CreateUpdateTicketResponseDto> CreateTicket(TicketInputDto ticketInputDto);
}