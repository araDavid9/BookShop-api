using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class PublisherDto
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Country { get; set; }
}