using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.DataAccessLayer.Entities;

public class SupporterRating
{
    public int SupporterId { get; set; }
    // public User Supporter { get; set; }

    public Rating Rating { get; set; }

    public int RatedUserId { get; set; }
    // public User RatedUser { get; set; }

    public int RelatedTicketId { get; set; }
    // public Ticket RelatedTicket { get; set; }
}