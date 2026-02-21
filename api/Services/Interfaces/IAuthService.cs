using api.DTOs;

namespace api.Services.Interfaces;

public interface IAuthService
{
    public Task<LoginResponseDto?> Login(LoginRequestDto loginRequestDto);
    public Task<LoginResponseDto?> Register(RegisterRequestDto registerRequestDto);
}