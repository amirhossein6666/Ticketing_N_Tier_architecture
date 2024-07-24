using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticketing.DataAccessLayer.Entities;

public class Message
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Body { get; set; }

    public DateTime SendDate { get; set; }

    public int SenderId { get; set; }
    public User Sender { get; set; }

    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }

    //self referencing for handling reply functionality
    public int? ParentMessageId { get; set; }
    public Message ParentMessage { get; set; }
    public ICollection<Message> Replies { get; set; }

    public bool IsDeleted { get; set; }
}