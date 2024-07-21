using AutoMapper;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.MessageDtos;

namespace Ticketing.businessLogicLayer.Services.Implementations;

public class MessageService: IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public MessageService(IMessageRepository messageRepository, IMapper mapper, IConfiguration config)
    {
        _messageRepository = messageRepository;
        _mapper = mapper;
        _config = config;
    }

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