using AutoMapper;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.Dtos.MessageDtos;

namespace Ticketing.businessLogicLayer.Tools;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        CreateMap<MessageInputDto, Message>();
    }
}