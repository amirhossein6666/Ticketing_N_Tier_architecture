using Ticketing.Dtos.MessageDtos;

namespace Ticketing.businessLogicLayer.Services.Interfaces;

public interface IMessageService
{
    public Task<MessageReturnDto> CreateMessage(MessageInputDto messageInputDto);
    public Task<MessageDto> GetMessageById(int id);
    public Task<ICollection<MessageDto>> GetMessagesByTicketId(int ticketId);
    public Task<ICollection<MessageDto>> GetMessagesByUserId(int userId);
    public Task<MessageReturnDto> UpdateMessage(int id, UpdateMessageDto updateMessageDto);
}