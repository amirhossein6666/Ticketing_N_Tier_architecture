using Ticketing.Dtos.ResponseDtos.UserResponseDtos;
using Ticketing.Dtos.UserDtos;

namespace Ticketing.businessLogicLayer.Services.Interfaces;

public interface IUserService
{
    public Task<CreateUpdateUserResponseDto> CreateUser(CreateUpdateUserInputDto createUpdateUserInputDto);
}