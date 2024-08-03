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
            .Where(u => u.Id == id && !u.IsDeleted)
            .Select(u => new User
            {
                Id = u.Id,
                Username = u.Username,
                password = u.password,
                Role = u.Role,
                CreatedTickets = u.CreatedTickets.Where(t => !t.IsDeleted).ToList(),
                AnsweredTicket = u.AnsweredTicket,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _appDbContext.Users
            .Where(u => u.Username == username && !u.IsDeleted)
            .Select(u => new User
            {
                Id = u.Id,
                Username = u.Username,
                password = u.password,
                Role = u.Role,
                CreatedTickets = u.CreatedTickets.Where(t => !t.IsDeleted).ToList(),
                AnsweredTicket = u.AnsweredTicket,
            })
            .FirstOrDefaultAsync();

    }

    public async Task<ICollection<User>> GetUsersByRole(Role role)
    {
        return await _appDbContext.Users.Where(u => u.Role == role && !u.IsDeleted).ToListAsync();
    }

    public async Task<User> UpdateUser(User updatedUser)
    {
        _appDbContext.Entry(updatedUser).State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();
        return updatedUser;
    }
}