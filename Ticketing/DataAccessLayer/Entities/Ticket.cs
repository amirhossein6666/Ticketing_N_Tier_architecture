using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.DataAccessLayer.Entities;

public class Ticket
{
    public int Id { get; set; }

    public Guid TrackingNumber { get; set; }

    public string Title { get; set; }

    public int CreatorId { get; set; }
    public User Creator { get; set; }

    public Rating? Rating { get; set; }

    public Status Status { get; set; }

    public ICollection<User> Supporters { get; set; }
    public ICollection<TicketSupporter> TicketSupporters { get; set; } = new List<TicketSupporter>();
    public ICollection<Message> Messages { get; set; }

    public bool IsDeleted { get; set; } = false;
}