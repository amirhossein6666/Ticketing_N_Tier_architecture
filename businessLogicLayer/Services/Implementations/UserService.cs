using AutoMapper;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.ResponseDtos.UserResponseDtos;
using Ticketing.Dtos.UserDtos;

namespace Ticketing.businessLogicLayer.Services.Implementations;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration config)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<CreateUpdateUserResponseDto> CreateUser(CreateUpdateUserInputDto createUpdateUserInputDto)
    {
        var user = _mapper.Map<User>(createUpdateUserInputDto);
        try
        {
            var returnedUser = await _userRepository.CreateUser(user);
            return new CreateUpdateUserResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status201Created,
                Message = $"user with id {returnedUser.Id} created ",
                Data = _mapper.Map<CreateUpdateUserDto>(returnedUser)
            };
        }
        catch (Exception e)
        {
            return new CreateUpdateUserResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.ToString(),
            };
        }
    }

    public async Task<UserResponseDto> GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserResponseDto> GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<UserListResponseDto> GetUsersByRole(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<CreateUpdateUserResponseDto> UpdateUser(CreateUpdateUserInputDto createUpdateUserInputDto)
    {
        throw new NotImplementedException();
    }

    public async Task<LoginResponseDto> Login(LoginInputDto loginInputDto)
    {
        throw new NotImplementedException();
    }

}
