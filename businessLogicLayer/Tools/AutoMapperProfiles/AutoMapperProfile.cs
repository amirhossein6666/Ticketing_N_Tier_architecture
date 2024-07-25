using AutoMapper;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.Dtos.MessageDtos;
using Ticketing.Dtos.TicketDtos;
using Ticketing.Dtos.UserDtos;

namespace Ticketing.businessLogicLayer.Tools.AutoMapperProfiles;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<MessageInputDto, Message>();
        CreateMap<Message, CreateUpdateMessageDto>();
        CreateMap<UpdateMessageDto, Message>();
        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.SenderUsername, opt => opt.MapFrom(src => src.Sender.Username))
            .ForMember(dest => dest.ParentMessageBody, opt => opt.MapFrom(src => src.ParentMessage.Body))
            .ForMember(dest => dest.ParentMessageSendDate, opt => opt.MapFrom(src => src.ParentMessage.SendDate))
            .ForMember(dest => dest.ParentMessageSenderUsername, opt => opt.MapFrom(src => src.ParentMessage.Sender.Username));

        CreateMap<TicketInputDto, Ticket>();
        CreateMap<Ticket, CreateUpdateTicketDto>();
        CreateMap<UpdateTicketInputDto, Ticket>();
        CreateMap<Ticket, TicketListDto>();
        CreateMap<Ticket, TicketDto>()
            .ForMember(dest => dest.CreatorUsername, opt => opt.MapFrom(src => src.Creator.Username))
            .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Messages));

        CreateMap<CreateUserInputDto, User>();
        CreateMap<UpdateUserInputDto, User>();
        CreateMap<User, CreateUpdateUserDto>();
        CreateMap<User, UserListDto>();
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.CreatedTickets, opt => opt.MapFrom(src => src.CreatedTickets))
            .ForMember(dest => dest.AnsweredTicket, opt => opt.MapFrom(src => src.AnsweredTicket));

    }
}