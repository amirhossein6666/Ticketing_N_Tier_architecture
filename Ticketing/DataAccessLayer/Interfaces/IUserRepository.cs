using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Enums;

namespace Ticketing.DataAccessLayer.Interfaces;

public interface IUserRepository
{
    public Task<User> CreateUser(User user);
    public Task<User?> GetUserById(int id);
    public Task<User?> GetUserByUsername(string username);
    public Task<ICollection<User>> GetUsersByRole(Role role);
    public Task<User> UpdateUser(User updatedUser);
}