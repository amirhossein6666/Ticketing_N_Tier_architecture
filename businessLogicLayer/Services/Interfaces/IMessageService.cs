using Ticketing.Dtos;

namespace Ticketing.businessLogicLayer.Services.Interfaces;

public interface IMessageService
{
    public Task<MessageReturnDto> CreateMessage(MessageInputDto messageInputDto);
}