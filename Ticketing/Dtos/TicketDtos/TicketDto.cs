using Ticketing.DataAccessLayer.Enums;
using Ticketing.Dtos.MessageDtos;

namespace Ticketing.Dtos.TicketDtos;

public class TicketDto
{
    public Guid TrackingNumber { get; set; }

    public string Title { get; set; }

    public string CreatorUsername { get; set; }

    public Rating? Rating { get; set; }

    public Status Status { get; set; }

    public ICollection<MessagesOfTicketDto> Messages { get; set; }
}