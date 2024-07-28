using Ticketing.DataAccessLayer.Entities;

namespace Ticketing.DataAccessLayer.Interfaces;

public interface IMessageRepository
{
    public Task<Message> CreateMessage(Message message);
    public Task<Message?> GetMessageById(int id);
    public Task<ICollection<Message>> GetMessagesByTicketId(int ticketId);
    public Task<ICollection<Message>> GetMessagesByUserId(int userId);
    public Task<Message> UpdateMessage( Message message);
}