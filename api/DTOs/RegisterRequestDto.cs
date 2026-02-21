using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class RegisterRequestDto
{
    [Required]
    public required string Username { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required]
    [MinLength(6)]
    public required string Password { get; set; }
}