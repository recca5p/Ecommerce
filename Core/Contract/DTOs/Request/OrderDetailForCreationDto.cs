using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class OrderDetailForCreationDto
{
    [Required]
    public long VariantId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; } // Số lượng sản phẩm
}
