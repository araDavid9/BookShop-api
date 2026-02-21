using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class OrderRequestDto
{
   [Required]
   public string BookId { get; set; }
   [Required]
   [Range(1, int.MaxValue)]
   public int Quantity { get; set; }
}