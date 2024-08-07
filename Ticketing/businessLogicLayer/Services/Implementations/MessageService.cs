using AutoMapper;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Enums;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.MessageDtos;
using Ticketing.Dtos.ResponseDtos.MessageResponseDtos;

namespace Ticketing.businessLogicLayer.Services.Implementations;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public MessageService(IMessageRepository messageRepository,ITicketRepository ticketRepository, IUserRepository userRepository,IMapper mapper)
    {
        _messageRepository = messageRepository;
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<CreateUpdateMessageResponseDto> CreateMessage(MessageInputDto messageInputDto)
    {
        var sender = await _userRepository.GetUserById(messageInputDto.SenderId);
        if (sender is null)
        {
            return new CreateUpdateMessageResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = $"user with id {messageInputDto.SenderId} as sender not found"
            };
        }

        var ticket = await _ticketRepository.GetTicketById(messageInputDto.TicketId);
        if (ticket is null)
        {
            return new CreateUpdateMessageResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = $"ticket with id {messageInputDto.TicketId} not found"
            };
        }
        if (ticket.Status != Status.Unread && ticket.Status != Status.Answered)
        {
            return new CreateUpdateMessageResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status405MethodNotAllowed,
                Message = "sending message is not allowed because of its ticektStatus"
            };

        }
        if (sender.Role != Role.Supporter && sender.Id != ticket.CreatorId)
        {
            return new CreateUpdateMessageResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status405MethodNotAllowed,
                Message = $"the user with id {sender.Id} is not allowed to send message in ticket with id {ticket.Id}"
            };
        }
        if (messageInputDto.ParentMessageId.HasValue)
        {
            var parentMessage = await _messageRepository.GetMessageById(messageInputDto.ParentMessageId.Value);
            if (parentMessage is null)
            {
                return new CreateUpdateMessageResponseDto()
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = $"message with id {messageInputDto.ParentMessageId} as parent message not found"
                };
            }

            if (parentMessage.TicketId != messageInputDto.TicketId)
            {
                return new CreateUpdateMessageResponseDto()
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = $"parent message with id {parentMessage.Id} doesn't belong to the this ticket"
                };
            }
        }
        var message = _mapper.Map<Message>(messageInputDto);
        message.SendDate = DateTime.Now;
        try
        {
            var returnedMessage = await _messageRepository.CreateMessage(message);
            if (sender.Role == Role.Supporter)
            {
                ticket.Status = Status.Answered;
                var flag = false;
                foreach (var ticketSupporter in ticket.Supporters)
                {
                    if (ticketSupporter.Id == sender.Id)
                    {
                        flag = true;
                    }
                }

                if (!flag)
                {
                    var ticketSupporter = new TicketSupporter()
                    {
                        TicketId = ticket.Id,
                        UserId = sender.Id
                    };
                    ticket.TicketSupporters.Add(ticketSupporter);
                    await _ticketRepository.UpdateTicket(ticket);
                }

            }
            return new CreateUpdateMessageResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status201Created,
                Message = $"message with id {returnedMessage.Id} Created",
                Data = _mapper.Map<CreateUpdateMessageDto>(returnedMessage),
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
        var messages = await _messageRepository.GetMessagesByTicketId(ticketId);
        var messageDtos = _mapper.Map<ICollection<MessageDto>>(messages);
        return new MessageListResponseDto()
        {
            IsSuccess = true,
            StatusCode = messageDtos.Count == 0? StatusCodes.Status404NotFound: StatusCodes.Status200OK,
            Message = messageDtos.Count == 0? "No Messages Found": $"{messageDtos.Count} messages found",
            Data = messageDtos
        };
    }

    public async Task<MessageListResponseDto> GetMessagesByUserId(int userId)
    {
        var messageDtos = _mapper.Map<ICollection<MessageDto>>(await _messageRepository.GetMessagesByUserId(userId));
        return new MessageListResponseDto()
        {
            IsSuccess = true,
            StatusCode =messageDtos.Count == 0? StatusCodes.Status404NotFound: StatusCodes.Status200OK,
            Message = messageDtos.Count == 0? "No Messages Found": $"{messageDtos.Count} messages found",
            Data = messageDtos
        };
    }

    public async Task<CreateUpdateMessageResponseDto> UpdateMessage(int id, UpdateMessageDto updateMessageDto)
    {
        var message = await _messageRepository.GetMessageById(id);
        if (message is null)
            return new CreateUpdateMessageResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"message with id {id} Not found"
            };
        _mapper.Map(updateMessageDto, message);
        try
        {
            var returnedMessage = await _messageRepository.UpdateMessage(message);
            return new CreateUpdateMessageResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = $"message with id {id} updated",
                Data = _mapper.Map<CreateUpdateMessageDto>(returnedMessage),
            };
        }
        catch (Exception e)
        {
            return new CreateUpdateMessageResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.ToString()
            };
        }
    }

    public async Task<DeleteMessageResponseDto> DeleteMessage(int id)
    {
        var message = await _messageRepository.GetMessageById(id);
        if (message is null)
            return new DeleteMessageResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"message with id {id} Not found"
            };
        message.IsDeleted = true;
        try
        {
            var returnedMessage = await _messageRepository.UpdateMessage(message);
            foreach (var messageReply in message.Replies)
            {
                messageReply.IsDeleted = true;
                await _messageRepository.UpdateMessage(messageReply);
            }
            return new DeleteMessageResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = $"message with id {id} removed",
            };
        }
        catch (Exception e)
        {
            return new DeleteMessageResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.ToString()
            };
        }
    }
}