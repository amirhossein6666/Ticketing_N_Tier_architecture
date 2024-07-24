using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.Dtos.UserDtos;

public class CreateUpdateUserInputDto
{
    public string Username { get; set; }

    public string password { get; set; }

    public Role Role { get; set; }
}