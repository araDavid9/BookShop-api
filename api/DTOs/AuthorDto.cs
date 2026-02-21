using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class AuthorDto
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Bio { get; set; }
    [Required]
    public required string Country { get; set; }
    [Required]
    [Range(1,120)]
    public required int Age { get; set; }
}