using api.Enums;

namespace api.DTOs;

public class LoginResponseDto
{
    public string Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public ERole Role { get; set; }
    public required string Token { get; set; }
}