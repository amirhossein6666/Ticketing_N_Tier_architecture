using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Enums;
using Ticketing.Dtos.TicketDtos;

namespace Ticketing.Dtos.UserDtos;

public class UserDto
{
    public string Username { get; set; }

    public Role Role { get; set; }

    public ICollection<TicketListDto> CreatedTickets { get; set; }

    public ICollection<TicketListDto> AnsweredTicket { get; set; }

}