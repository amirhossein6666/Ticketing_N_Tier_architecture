using Ticketing.Dtos.MessageDtos;
using Ticketing.Dtos.ResponseDtos.MessageResponseDtos;

namespace Ticketing.businessLogicLayer.Services.Interfaces;

public interface IMessageService
{
    public Task<CreateUpdateMessageResponseDto> CreateMessage(MessageInputDto messageInputDto);
    public Task<MessageResponseDto> GetMessageById(int id);
    public Task<MessageListResponseDto> GetMessagesByTicketId(int ticketId);
    public Task<MessageListResponseDto> GetMessagesByUserId(int userId);
    public Task<CreateUpdateMessageResponseDto> UpdateMessage(int id, UpdateMessageDto updateMessageDto);
}