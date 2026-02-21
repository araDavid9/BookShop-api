using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class BookDto
{ 
    [Required]
    public required string Name { get; set; }
    [Required]
    [Range(1, int.MaxValue)]
    public required decimal Price { get; set; }
    [Range(0, int.MaxValue)]
    public required int Stock { get; set; }
}