using Ticketing.Dtos.MessageDtos;
using Ticketing.Dtos.ResponseDtos.MessageResponseDtos;

namespace Ticketing.businessLogicLayer.Services.Interfaces;

public interface IMessageService
{
    public Task<MessageReturnResponseDto> CreateMessage(MessageInputDto messageInputDto);
    public Task<MessageResponseDto> GetMessageById(int id);
    public Task<ICollection<MessageDto>> GetMessagesByTicketId(int ticketId);
    public Task<ICollection<MessageDto>> GetMessagesByUserId(int userId);
    public Task<MessageReturnDto> UpdateMessage(int id, UpdateMessageDto updateMessageDto);
}