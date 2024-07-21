using Ticketing.DataAccessLayer.Entities;

namespace Ticketing.Dtos.MessageDtos;

public class MessageDto
{
    public int Id { get; set; }

    public string Body { get; set; }

    public DateTime SendDate { get; set; }

    public int SenderId { get; set; }
    public User User { get; set; }

    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }

    //self referencing for handling reply functionality
    public int? ParentMessageId { get; set; }
    public Message ParentMessage { get; set; }
    public ICollection<Message> Replies { get; set; }

}