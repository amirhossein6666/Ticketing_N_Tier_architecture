using Ticketing.Dtos.MessageDtos;

namespace Ticketing.businessLogicLayer.Services.Interfaces;

public interface IMessageService
{
    public Task<MessageReturnDto> CreateMessage(MessageInputDto messageInputDto);
    public Task<MessageDto> GetMessageById(int id);
}