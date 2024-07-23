using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.businessLogicLayer.Tools.CustomExceptions;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.MessageDtos;
using Ticketing.Dtos.ResponseDtos.MessageResponseDtos;

namespace Ticketing.businessLogicLayer.Services.Implementations;

public class MessageService : IMessageService
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

    public async Task<CreateUpdateMessageResponseDto> CreateMessage(MessageInputDto messageInputDto)
    {
        var message = _mapper.Map<Message>(messageInputDto);
        message.SendDate = DateTime.Now;
        try
        {
            var returnedMessage = await _messageRepository.CreateMessage(message);
            return new CreateUpdateMessageResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status201Created,
                Message = $"message with id {returnedMessage.Id} Created",
                Data = _mapper.Map<MessageReturnDto>(returnedMessage),
            };
        }
        catch (Exception e)
        {
            return new CreateUpdateMessageResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.ToString(),
            };
        }
    }

    public async Task<MessageResponseDto> GetMessageById(int id)
    {
        var message = await _messageRepository.GetMessageById(id);
        if (message is null)
            return new MessageResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = "message Not Found"
            };
        return new MessageResponseDto()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Ok",
            Data = _mapper.Map<MessageDto>(message)
        };
    }

    public async Task<MessageListResponseDto> GetMessagesByTicketId(int ticketId)
    {
        var messageDtos = _mapper.Map<ICollection<MessageDto>>(await _messageRepository.GetMessagesByTicketId(ticketId));
        return new MessageListResponseDto()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Message = $"{messageDtos.Count} messages found",
            Data = messageDtos
        };
    }

    public async Task<MessageListResponseDto> GetMessagesByUserId(int userId)
    {
        var messageDtos = _mapper.Map<ICollection<MessageDto>>(await _messageRepository.GetMessagesByUserId(userId));
        return new MessageListResponseDto()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Message = $"{messageDtos.Count} messages found",
            Data = messageDtos
        };
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