using Ticketing.Dtos.ResponseDtos.TicketResponseDtos;
using Ticketing.Dtos.TicketDtos;

namespace Ticketing.businessLogicLayer.Services.Interfaces;

public interface ITicketService
{
    public Task<CreateUpdateTicketResponseDto> CreateTicket(TicketInputDto ticketInputDto);
    public Task<TicketResponseDto> GetTicketById(int id);
    public Task<TicketListResponseDto> GetAllTickets();
    public Task<TicketListResponseDto> GetTicketsByCreatorId(int CreatorId);
    public Task<CreateUpdateTicketResponseDto> UpdateTicket(int id, UpdateTicketInputDto updateTicketInputDto);
}