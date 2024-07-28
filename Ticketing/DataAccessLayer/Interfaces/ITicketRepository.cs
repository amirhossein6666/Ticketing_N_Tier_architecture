using Ticketing.DataAccessLayer.Entities;

namespace Ticketing.DataAccessLayer.Interfaces;

public interface ITicketRepository
{
    public Task<Ticket> CreateTicket(Ticket ticket);
    public Task<Ticket?> GetTicketById(int id);
    public Task<ICollection<Ticket>> GetAllTickets();
    public Task<ICollection<Ticket>> GetTicketsByCreatorId(int creatorId);
    public Task<Ticket> UpdateTicket(Ticket updatedTicket);
}