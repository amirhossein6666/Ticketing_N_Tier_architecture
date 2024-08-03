using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.Dtos.UserDtos;

public class UserSetRatingDto
{
    public int SupporterId { get; set; }

    public Rating Rating { get; set; }

    public int RatedUserId { get; set; }

    public int RelatedTicketId { get; set; }
}