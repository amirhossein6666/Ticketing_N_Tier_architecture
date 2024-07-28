using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.Dtos.UserDtos;

public class UserListDto
{
    public int Id { get; set; }

    public string Username { get; set; }

    public Role Role { get; set; }
}