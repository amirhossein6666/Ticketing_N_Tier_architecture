using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.Dtos.TicketDtos;

public class CreateUpdateTicketDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int CreatorId { get; set; }

    public Rating? Rating { get; set; }

    public Status Status { get; set; }

}