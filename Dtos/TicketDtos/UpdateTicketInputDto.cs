using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.Dtos.TicketDtos;

public class UpdateTicketInputDto
{
    public string Title { get; set; }

    public Rating? Rating { get; set; }

    public Status Status { get; set; }

}