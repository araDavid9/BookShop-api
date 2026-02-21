using api.DTOs;
using api.Models;
using api.Repositories.Interfaces;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace api.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthService> _logger;
    
    
    public AuthService(IUserRepository userRepository, ITokenService tokenService, ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _logger = logger;
    }
    
    public async Task<LoginResponseDto?> Login(LoginRequestDto loginRequestDto)
    {
        _logger.LogInformation("[AuthSerivce] Getting Login Request for:{0}", loginRequestDto.Email);
        if(string.IsNullOrEmpty(loginRequestDto.Email) || string.IsNullOrEmpty(loginRequestDto.Password))
            return null;
        
        var currentUser = await _userRepository.GetUserByEmail(loginRequestDto.Email);
        if (currentUser == null)
        {
            _logger.LogInformation("[AuthSerivce] Login Request Failed for:{0}", loginRequestDto.Email);
            return null;
        }
          
        
        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(currentUser,currentUser.Password,loginRequestDto.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }

        var token = await _tokenService.GenerateToken(currentUser);
        return new LoginResponseDto
        {
            Id = currentUser.Id,
            Email = currentUser.Email,
            Username = currentUser.Username,
            Role = currentUser.Role,
            Token = token,
        };
    }

    public async Task<LoginResponseDto?> Register(RegisterRequestDto registerRequestDto)
    {
        _logger.LogInformation("[AuthService] Getting Registration Request for:{0}", registerRequestDto.Email);
        var currentUser = await _userRepository.GetUserByEmail(registerRequestDto.Email);
        if (currentUser != null)
        {
            _logger.LogInformation("[AuthService] Seems like Email Already Exists");
            return null;
        }
          

        var newUser = new User
        {
            Email = registerRequestDto.Email,
            Username = registerRequestDto.Username,
            Password = registerRequestDto.Password,
        };
        
        var hasher = new PasswordHasher<User>();
        newUser.Password = hasher.HashPassword(newUser, newUser.Password);
        
        await _userRepository.CreateUser(newUser);
        var token = await _tokenService.GenerateToken(newUser);
        return new LoginResponseDto
        {
            Id = newUser.Id,
            Email = newUser.Email,
            Username = newUser.Username,
            Role = newUser.Role,
            Token = token,
        };
    }
}