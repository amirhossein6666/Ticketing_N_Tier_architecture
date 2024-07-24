using System.IdentityModel.Tokens.Jwt;
using Ticketing.Dtos.BaseDtos;

namespace Ticketing.Dtos.ResponseDtos.UserResponseDtos;

public class LoginResponseDto: BaseResponseDto<JwtSecurityToken>
{

}