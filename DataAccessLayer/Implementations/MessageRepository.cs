using Microsoft.EntityFrameworkCore;
using Ticketing.DataAccessLayer.Context;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Interfaces;

namespace Ticketing.DataAccessLayer.Implementations;

public class MessageRepository: IMessageRepository
{
    private readonly AppDbContext _appDbContext;

    public MessageRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Message> CreateMessage(Message message)
    {
        _appDbContext.Messages.Add(message);
        await _appDbContext.SaveChangesAsync();
        return message;
    }

    public async Task<Message?> GetMessageById(int id)
    {
        return await _appDbContext.Messages
            .Include(m => m.Sender)
            .Include(m => m.Ticket)
            .Include(m => m.ParentMessage)
            .Include(m => m.Replies)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<ICollection<Message>> GetMessagesByTicketId(int ticketId)
    {
        return await _appDbContext.Messages.Where(m => m.TicketId == ticketId).ToListAsync();
    }

    public async Task<ICollection<Message>> GetMessagesByUserId(int userId)
    {
        return await _appDbContext.Messages.Where(m => m.SenderId == userId).ToListAsync();
    }

    public async Task<Message> UpdateMessage(Message message)
    {
        _appDbContext.Entry(message).State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();
        return message;
    }
}