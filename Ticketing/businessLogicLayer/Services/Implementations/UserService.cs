using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Enums;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.ResponseDtos.UserResponseDtos;
using Ticketing.Dtos.UserDtos;

namespace Ticketing.businessLogicLayer.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly ISupporterRatingRepository _supporterRatingRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly IValidator<CreateUserInputDto> _createUserInputDtoValidator;
    private readonly IValidator<UpdateUserInputDto> _updateUserInputDtoValidator;

    public UserService(IUserRepository userRepository, ITicketRepository ticketRepository, ISupporterRatingRepository supporterRatingRepository,  IMapper mapper, IConfiguration config, IValidator<CreateUserInputDto> createUserInputDtoValidator, IValidator<UpdateUserInputDto> updateUserInputDtoValidator)
    {
        _userRepository = userRepository;
        _ticketRepository = ticketRepository;
        _supporterRatingRepository = supporterRatingRepository;
        _mapper = mapper;
        _config = config;
        _createUserInputDtoValidator = createUserInputDtoValidator;
        _updateUserInputDtoValidator = updateUserInputDtoValidator;
    }

    public async Task<CreateUpdateUserResponseDto> CreateUser(CreateUserInputDto createUserInputDto)
    {
        var validationResult = await _createUserInputDtoValidator.ValidateAsync(createUserInputDto);

        if (!validationResult.IsValid)
        {
            return new CreateUpdateUserResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
            };
        }
        if (createUserInputDto.Role != Role.Supporter && createUserInputDto.Role != Role.Client)
        {
            return new CreateUpdateUserResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "invalid value for Role enum"
            };
        }
        var user = _mapper.Map<User>(createUserInputDto);
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
            Data = _mapper.Map<UserDto>(user)
        };
    }

    public async Task<UserListResponseDto> GetUsersByRole(string role)
    {
        Role roleEnum;
        switch (role)
        {
            case "Supporter":
                roleEnum = Role.Supporter;
                break;
            case "Client":
                roleEnum = Role.Client;
                break;
            default:
                return new UserListResponseDto()
                {
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Invalid value for Role enum"
                };
        }
        var userDtos = _mapper.Map<ICollection<UserListDto>>(await _userRepository.GetUsersByRole(roleEnum));
        return new UserListResponseDto()
        {
            IsSuccess = true,
            StatusCode = userDtos.Count == 0 ? StatusCodes.Status404NotFound : StatusCodes.Status200OK,
            Message = userDtos.Count == 0 ? "No users found" : $"{userDtos.Count} user found",
            Data = userDtos,
        };
    }

    public async Task<CreateUpdateUserResponseDto> UpdateUser(UpdateUserInputDto updateUserInputDto, int id)
    {
        var validationResult = await _updateUserInputDtoValidator.ValidateAsync(updateUserInputDto);

        if (!validationResult.IsValid)
        {
            return new CreateUpdateUserResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
            };
        }
        var user = await _userRepository.GetUserById(id);
        if (user is null)
            return new CreateUpdateUserResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = "user not found"
            };
        _mapper.Map(updateUserInputDto, user);
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

    public async Task<DeleteUserResponseDto> DeleteUser(int id)
    {
        var user = await _userRepository.GetUserById(id);
        if (user is null)
            return new DeleteUserResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = "user not found"
            };
        user.IsDeleted = true;
        try
        {
            await _userRepository.UpdateUser(user);
            return new DeleteUserResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = $"user with it {id} removed"
            };
        }
        catch (Exception e)
        {
            return new DeleteUserResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = e.ToString()
            };
        }
    }

    public async Task<UserSetRatingResponseDto> UserSetRating(UserSetRatingInputDto userSetRatingInputDto)
    {
        var ratedUser = await _userRepository.GetUserById(userSetRatingInputDto.RatedUserId);
        if (ratedUser is null)
            return new UserSetRatingResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"user with id {userSetRatingInputDto.RatedUserId} as Rated User not found"
            };
        var supporter = await _userRepository.GetUserById(userSetRatingInputDto.SupporterId);
        if (supporter is null)
            return new UserSetRatingResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"user with id {userSetRatingInputDto.SupporterId} as Supporter not found "
            };
        var relatedTicket = await _ticketRepository.GetTicketById(userSetRatingInputDto.RelatedTicketId);
        if (relatedTicket is null)
        {
            return new UserSetRatingResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"ticket with id {userSetRatingInputDto.RelatedTicketId} not found"
            };
        }

        if (supporter.Role != Role.Supporter)
        {
            return new UserSetRatingResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = $"user with id {supporter.Id} is not supporter"
            };
        }

        if (ratedUser.Role != Role.Client)
        {
            return new UserSetRatingResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = $"user with id {ratedUser.Id} is not Client"
            };
        }

        if (ratedUser.Id != relatedTicket.CreatorId)
        {
            return new UserSetRatingResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status405MethodNotAllowed,
                Message = $"user with id {ratedUser.Id} as rated User not allowed to set rating for supporters"
            };
        }
        if (relatedTicket.Supporters.All(u => u.Id != supporter.Id))
        {
            return new UserSetRatingResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = $"this user is not a supporter of this ticket"
            };
        }
        if (relatedTicket.Status is not (Status.Answered or Status.Closed or Status.Open))
        {
            return new UserSetRatingResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message =
                    $"the status of ticket with id {relatedTicket.Id} is not appropriate for setting rating for its supporters"
            };
        }

        if (userSetRatingInputDto.Rating is not (Rating.OneStar or Rating.TwoStar or Rating.ThreeStar or Rating.FourStar or Rating.FiveStar ))
        {
            return new UserSetRatingResponseDto()
            {
                IsSuccess = false,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Invalid value for Rating enum"
            };
        }

        try
        {
            var supporterRating = new SupporterRating()
            {
                SupporterId = userSetRatingInputDto.RatedUserId,
                RelatedTicketId = userSetRatingInputDto.RelatedTicketId,
                RatedUserId = userSetRatingInputDto.RatedUserId,
                Rating = userSetRatingInputDto.Rating
            };
            await _supporterRatingRepository.AddRating(supporterRating);
            return new UserSetRatingResponseDto()
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = $"set rating for supporter with id {supporter.Id} for answering ticket {relatedTicket.Id}"
            };
        }
        catch (Exception e)
        {
            return new UserSetRatingResponseDto()
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
            Data = GenerateToken(loginInputDto.Username, user.Id)
        };
    }

    private string GenerateToken(string username, int userId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("userId", userId.ToString()),
        };

         var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}