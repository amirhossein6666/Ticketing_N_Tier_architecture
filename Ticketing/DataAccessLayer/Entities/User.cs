using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.DataAccessLayer.Entities;
[Index(nameof(Username), IsUnique = true)]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Username { get; set; }

    public string password { get; set; }

    public Role Role { get; set; }

    public ICollection<Ticket> CreatedTickets { get; set; }

    public ICollection<Ticket> AnsweredTicket { get; set; }

    public ICollection<Message> Messages { get; set; }

    public bool IsDeleted { get; set; } = false;
}