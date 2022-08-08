using Identity.Api.Domain.Entities;

namespace Identity.Api.Domain.Common.Interfaces;

public interface IUserRepository
{
    User? GetUser(string Username);
    void AddUser(User user);
}