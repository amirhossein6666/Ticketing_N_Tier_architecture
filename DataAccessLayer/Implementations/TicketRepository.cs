using Microsoft.EntityFrameworkCore;
using Ticketing.DataAccessLayer.Context;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Interfaces;

namespace Ticketing.DataAccessLayer.Implementations;

public class TicketRepository: ITicketRepository
{
    private readonly AppDbContext _appDbContext;

    public TicketRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Ticket> CreateTicket(Ticket ticket)
    {
        _appDbContext.Tickets.Add(ticket);
        await _appDbContext.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket?> GetTicketById(int id)
    {
        return await _appDbContext.Tickets
            .Include(t => t.Messages)
            .Include(t => t.Creator)
            .Include(t => t.Supporters)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<ICollection<Ticket>> GetAllTickets()
    {
        return await _appDbContext.Tickets.ToListAsync();
    }

    public async Task<ICollection<Ticket>> GetTicketsByCreatorId(int creatorId)
    {
        throw new NotImplementedException();
    }

    public async Task<Ticket> UpdateTicket(Ticket updatedTicket)
    {
        throw new NotImplementedException();
    }
}