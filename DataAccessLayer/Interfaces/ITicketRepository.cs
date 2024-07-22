using Ticketing.DataAccessLayer.Entities;

namespace Ticketing.DataAccessLayer.Interfaces;

public interface ITicketRepository
{
    public Task<Ticket> CreateTicket(Ticket ticket);
    public Task<Ticket> GetTicketById(int id);
    public Task<ICollection<Ticket>> GetAllTickets();
}