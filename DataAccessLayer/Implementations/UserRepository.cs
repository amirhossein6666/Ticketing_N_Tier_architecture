using Microsoft.EntityFrameworkCore;
using Ticketing.DataAccessLayer.Context;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Enums;
using Ticketing.DataAccessLayer.Interfaces;

namespace Ticketing.DataAccessLayer.Implementations;

public class UserRepository: IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<User> CreateUser(User user)
    {
        _appDbContext.Users.Add(user);
        await _appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _appDbContext.Users
            .Include(u => u.CreatedTickets)
            .Include(u => u.AnsweredTicket)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _appDbContext.Users
            .Include(u => u.CreatedTickets)
            .Include(u => u.AnsweredTicket)
            .FirstOrDefaultAsync(u => u.Username == username);

    }

    public async Task<ICollection<User>> GetUsersByRole(Role role)
    {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}