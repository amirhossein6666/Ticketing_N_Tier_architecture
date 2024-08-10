using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.Dtos.UserDtos;

public class CreateUserInputDto
{
    public string? Username { get; set; }

    public string? password { get; set; }

    public Role? Role { get; set; }

}