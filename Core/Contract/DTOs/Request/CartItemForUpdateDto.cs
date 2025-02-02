using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class CartItemForUpdateDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }
}
