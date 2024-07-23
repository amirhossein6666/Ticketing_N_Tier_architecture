using AutoMapper;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.Dtos.MessageDtos;

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
    }
}