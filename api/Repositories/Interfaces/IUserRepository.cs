using api.Models;

namespace api.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<User> CreateUser(User user); 
    public Task<User?> GetUserByEmail(string email);
}