using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.businessLogicLayer.Tools.CustomExceptions;
using Ticketing.DataAccessLayer.Entities;
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
        var message = _mapper.Map<Message>(messageInputDto);
        message.SendDate = DateTime.Now;
        try
        {
            var returnedMessage = await _messageRepository.CreateMessage(message);
            return _mapper.Map<MessageReturnDto>(returnedMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<MessageDto> GetMessageById(int id)
    {
        var message = await _messageRepository.GetMessageById(id);
        if (message is null)
            throw new NotFoundException($"Message with id {id} not found.");
        return _mapper.Map<MessageDto>(message);
    }

    public async Task<ICollection<MessageDto>> GetMessagesByTicketId(int ticketId)
    {
        return _mapper.Map<ICollection<MessageDto>>(await _messageRepository.GetMessagesByTicketId(ticketId));
    }

    public async Task<ICollection<MessageDto>> GetMessagesByUserId(int userId)
    {
        return _mapper.Map<ICollection<MessageDto>>(await _messageRepository.GetMessagesByUserId(userId));
    }

    public async Task<MessageReturnDto> UpdateMessage(int id, UpdateMessageDto updateMessageDto)
    {
        var message = await _messageRepository.GetMessageById(id);
        if (message is null)
            throw new NotFoundException($"Message with id {id} not found.");
        _mapper.Map(updateMessageDto, message);
        try
        {
            var returnedMessage = await _messageRepository.UpdateMessage(message);
            return _mapper.Map<MessageReturnDto>(returnedMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}