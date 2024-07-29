using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.Dtos.TicketDtos;

public class SetTicketRatingDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public Rating? Rating { get; set; }

}