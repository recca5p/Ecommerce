using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class OrderDetailForCreationDto
{
    [Required]
    public long ProductId { get; set; } // ID của sản phẩm

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; } // Số lượng sản phẩm

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
    public decimal Price { get; set; } // Giá của sản phẩm
}
