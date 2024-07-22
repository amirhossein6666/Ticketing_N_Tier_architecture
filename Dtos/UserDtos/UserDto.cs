using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.Dtos.UserDtos;

public class UserDto
{
    public string Username { get; set; }

    public Role Role { get; set; }
}