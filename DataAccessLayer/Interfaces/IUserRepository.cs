using Ticketing.DataAccessLayer.Entities;

namespace Ticketing.DataAccessLayer.Interfaces;

public interface IUserRepository
{
    public Task<User> CreateUser(User user);
}