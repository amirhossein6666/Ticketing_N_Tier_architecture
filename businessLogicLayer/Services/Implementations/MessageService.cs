using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.Dtos.MessageDtos;

namespace Ticketing.businessLogicLayer.Services.Implementations;

public class MessageService: IMessageService
{

    public async Task<MessageReturnDto> CreateMessage(MessageInputDto messageInputDto)
    {
    }

    public async Task<MessageDto> GetMessageById(int id)
    {
    }

    public async Task<ICollection<MessageDto>> GetMessagesByTicketId(int ticketId)
    {
    }

    public async Task<ICollection<MessageDto>> GetMessagesByUserId(int userId)
    {
    }

    public async Task<MessageReturnDto> UpdateMessage(int id, UpdateMessageDto updateMessageDto)
    {
    }
}