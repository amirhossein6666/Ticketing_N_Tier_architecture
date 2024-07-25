using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Enums;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.ResponseDtos.UserResponseDtos;
using Ticketing.Dtos.UserDtos;

namespace Ticketing.businessLogicLayer.Services.Implementations;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration config)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _config = config;
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
        var user = await _userRepository.GetUserById(id);
        if (user is null)
            return new UserResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = "user not found"
            };
        return new UserResponseDto()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Ok",
            Data = _mapper.Map<UserDto>(user),
        };
    }

    public async Task<UserResponseDto> GetUserByUsername(string username)
    {
        var user = await _userRepository.GetUserByUsername(username);
        if (user is null)
            return new UserResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = "user not found"
            };
        return new UserResponseDto()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Ok",
            Data = _mapper.Map<UserDto>(user),
        };    }

    public async Task<UserListResponseDto> GetUsersByRole(string role)
    {
        if (!Enum.TryParse<Role>(role, true, out var roleEnum))
            return new UserListResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = $"{role} is not a valid Value for Role enum"
            };
        var userDtos = _mapper.Map<ICollection<UserListDto>>(await _userRepository.GetUsersByRole(roleEnum));
        return new UserListResponseDto()
        {
            IsSuccess = true,
            StatusCode = userDtos.Count == 0 ? StatusCodes.Status404NotFound : StatusCodes.Status200OK,
            Message = userDtos.Count == 0 ? "No users found" : $"{userDtos.Count} user found",
            Data = userDtos,
        };

    }

    public async Task<CreateUpdateUserResponseDto> UpdateUser(CreateUpdateUserInputDto createUpdateUserInputDto, int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user is null)
            return new CreateUpdateUserResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = "user not found"
            };
        _mapper.Map(createUpdateUserInputDto, user);
        try
        {
            var returnedUser = await _userRepository.UpdateUser(user);
            return new CreateUpdateUserResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = $"user with id {returnedUser.Id} updated",
                Data = _mapper.Map<CreateUpdateUserDto>(returnedUser),
            };
        }
        catch (Exception e)
        {
            return new CreateUpdateUserResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.ToString()
            };
        }
    }

    public async Task<LoginResponseDto> Login(LoginInputDto loginInputDto)
    {
        var user = await _userRepository.GetUserByUsername(loginInputDto.Username);
        if (user is null)
            return new LoginResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = "user with this username not found"
            };
        if (!user.password.Equals(loginInputDto.password))
            return new LoginResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = "username and password doesn't match"
            };
        return new LoginResponseDto()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Ok",
            Data = GenerateToken(loginInputDto.Username)
        };
    }
    private JwtSecurityToken GenerateToken(string username)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return  new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        // return new JwtSecurityTokenHandler().WriteToken(token);

    }

}

