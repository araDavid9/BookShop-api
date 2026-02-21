using api.Models;

namespace api.Services.Interfaces;

public interface ITokenService
{
    public Task<string> GenerateToken(User user);
    public Task<string> GetUserIdFromToken();
}