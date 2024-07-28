using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.Dtos.UserDtos;

public class CreateUpdateUserDto
{
    public int Id { get; set; }

    public string Username { get; set; }

    public Role Role { get; set; }

}