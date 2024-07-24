using AutoMapper;
using Ticketing.businessLogicLayer.Services.Interfaces;
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

    public async Task<LoginResponseDto> Login(LoginInputDto loginInputDto)
    {
        throw new NotImplementedException();
    }

    public async Task<CreateUpdateUserResponseDto> CreateUser(CreateUpdateUserInputDto createUpdateUserInputDto)
    {
        throw new NotImplementedException();
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
}