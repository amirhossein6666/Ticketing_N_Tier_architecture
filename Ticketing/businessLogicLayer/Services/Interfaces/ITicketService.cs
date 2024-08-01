using Ticketing.Dtos.ResponseDtos.TicketResponseDtos;
using Ticketing.Dtos.TicketDtos;

namespace Ticketing.businessLogicLayer.Services.Interfaces;

public interface ITicketService
{
    public Task<CreateUpdateTicketResponseDto> CreateTicket(TicketInputDto ticketInputDto);
    public Task<TicketResponseDto> GetTicketById(int id);
    public Task<TicketListResponseDto> GetAllTickets();
    public Task<TicketListResponseDto> GetTicketsByCreatorId(int creatorId);
    public Task<CreateUpdateTicketResponseDto> UpdateTicket(int id, UpdateTicketInputDto updateTicketInputDto);
    public Task<SetTicketRatingResponseDto> SetTicketRating(int ticketId, int rating);
    public Task<DeleteTicketResponseDto> DeleteTicket(int id);
    public Task<FinishTicketResponseDto> FinishTicket(FinishTicketInputDto finishTicketInputDto);
}