using AutoMapper;
using Moq;
using Ticketing.businessLogicLayer.Services.Implementations;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Enums;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.MessageDtos;

namespace TestProject1;

public class MessageTest
{
    private readonly MessageService _messageService;
    private readonly Mock<IMessageRepository> _messageRepoMock = new Mock<IMessageRepository>();
    private readonly Mock<ITicketRepository> _ticketRepoMock = new Mock<ITicketRepository>();
    private readonly Mock<IUserRepository> _userRepoMock = new Mock<IUserRepository>();
    private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();

    public MessageTest()
    {
        _messageService = new MessageService(_messageRepoMock.Object, _ticketRepoMock.Object, _userRepoMock.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetMessageByIdShouldReturnMessageWhenMessageExist()
    {
        // Arrange
        const int messageId = 17;
        var messageSender = new User()
        {
            Id = 4,
            Username = "four",
            password = "four",
            IsDeleted = false,
            Role = Role.Client
        };
        var parentMessageSender = new User()
        {
            Username = "three"
        };
        var messageParent = new Message()
        {
            Body = "string",
            SendDate = DateTime.Parse("2024-08-05 14:37:26.7247303"),
            Sender = parentMessageSender
        };
        var message = new Message()
        {
            Id = 17,
            Body = "string",
            SendDate = DateTime.Parse("2024-08-05 14:38:25.9281906"),
            SenderId = 4,
            TicketId = 9,
            ParentMessageId = 16,
            IsDeleted = false,
            Sender = messageSender,
            ParentMessage = messageParent
        };
        var messageDto = new MessageDto()
        {
            Body = message.Body,
            SendDate = message.SendDate,
            SenderUsername = message.Sender.Username,
            ParentMessageBody = message.ParentMessage.Body,
            ParentMessageSendDate = message.ParentMessage.SendDate,
            ParentMessageSenderUsername = message.ParentMessage.Sender.Username
        };
        _messageRepoMock.Setup(mr => mr.GetMessageById(messageId)).ReturnsAsync(message);
        _mockMapper.Setup(mapper => mapper.Map<MessageDto>(It.IsAny<Message>()))
            .Returns((Message src) => new MessageDto
            {
                Body = src.Body,
                SendDate = src.SendDate,
                SenderUsername = src.Sender.Username,
                ParentMessageBody = src.ParentMessage.Body,
                ParentMessageSendDate = src.ParentMessage.SendDate,
                ParentMessageSenderUsername = src.ParentMessage.Sender.Username
        });

        // Act
        var result = await _messageService.GetMessageById(messageId);

        // Assert
        Assert.Equal(messageDto.Body, result.Data.Body);
        Assert.Equal(messageDto.SendDate, result.Data.SendDate);
        Assert.Equal(messageDto.SenderUsername, result.Data.SenderUsername);
        Assert.Equal(messageDto.ParentMessageBody, result.Data.ParentMessageBody);
        Assert.Equal(messageDto.ParentMessageSendDate, result.Data.ParentMessageSendDate);
        Assert.Equal(messageDto.ParentMessageSenderUsername, result.Data.ParentMessageSenderUsername);
    }




    // [Fact]
    // public void FailingTest()
    // {
    //     Assert.Equal(5, 2 + 3);
    // }
}