using Identity.Api.Application.Common.Interfaces;
using Identity.Api.Domain.Entities;

namespace Identity.Api.Infrastructure;
using System.Collections.Concurrent;
public class UserRepository : IUserRepository
{
    private static readonly ConcurrentBag<User> _users = new();
    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public User? GetUser(string Username)
    {
        return _users.SingleOrDefault(u => u.Username == Username);
    }
}