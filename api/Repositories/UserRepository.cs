using api.Data;
using api.Models;
using api.Repositories.Interfaces;

namespace api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BookStoreDbContext _dbContext;
    
    public UserRepository(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User> CreateUser(User user)
    {
        var isExistingEmail = _dbContext.Users.FirstOrDefault(x=> x.Email == user.Email);
        if (isExistingEmail != null)
        {
            throw new AggregateException("email already exists");
        }
        
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return _dbContext.Users.FirstOrDefault(x=> x.Email == email);
    }
}