using AutoMapper;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.Dtos.MessageDtos;

namespace Ticketing.businessLogicLayer.Tools.AutoMapperProfiles;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<MessageInputDto, Message>();
        CreateMap<Message, MessageReturnDto>();
        CreateMap<Message, MessageDto>();
        CreateMap<UpdateMessageDto, Message>();
    }
}