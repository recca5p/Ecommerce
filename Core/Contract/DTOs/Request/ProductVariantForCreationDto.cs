using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class ProductVariantForCreationDto
{
    [Required(ErrorMessage = "Color is required")]
    public string Color { get; set; }

    [Required(ErrorMessage = "Storage is required")]
    public string Storage { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Stock is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative value")]
    public int Stock { get; set; }
}
