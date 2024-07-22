using Ticketing.DataAccessLayer.Entities;

namespace Ticketing.DataAccessLayer.Interfaces;

public interface ITicketRepository
{
    public Task<Ticket> CreateTicket(Ticket ticket);
}