using Ticketing.DataAccessLayer.Enums;
using Ticketing.Dtos.ResponseDtos.UserResponseDtos;
using Ticketing.Dtos.UserDtos;

namespace Ticketing.businessLogicLayer.Services.Interfaces;

public interface IUserService
{
    public Task<LoginResponseDto> Login(LoginInputDto loginInputDto);
    public Task<CreateUpdateUserResponseDto> CreateUser(CreateUserInputDto createUserInputDto);
    public Task<UserResponseDto> GetUserById(int id);
    public Task<UserResponseDto> GetUserByUsername(string username);
    public Task<UserListResponseDto> GetUsersByRole(string  role);
    public Task<CreateUpdateUserResponseDto> UpdateUser(UpdateUserInputDto updateUserInputDto, int id);
    public Task<DeleteUserResponseDto> DeleteUser(int id);
    public Task<UserSetRatingResponseDto> UserSetRating(UserSetRatingInputDto userSetRatingInputDto);
}