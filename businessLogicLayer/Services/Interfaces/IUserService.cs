using Ticketing.Dtos.ResponseDtos.UserResponseDtos;
using Ticketing.Dtos.UserDtos;

namespace Ticketing.businessLogicLayer.Services.Interfaces;

public interface IUserService
{
    public Task<CreateUpdateUserResponseDto> CreateUser(CreateUpdateUserInputDto createUpdateUserInputDto);
    public Task<UserResponseDto> GetUserById(int id);
    public Task<UserResponseDto> GetUserByUsername(string username);
    public Task<UserListResponseDto> GetUsersByRole(string username);
    public Task<CreateUpdateUserResponseDto> UpdateUser(CreateUpdateUserInputDto createUpdateUserInputDto);
}