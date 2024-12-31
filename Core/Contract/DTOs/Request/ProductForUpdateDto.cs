using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class ProductForUpdateDto
{
    [Required(ErrorMessage = "Product name is required")]
    [MinLength(1)]
    public string ProductName { get; set; }
    [MinLength(1)]
    public string Description { get; set; }
    [Required(ErrorMessage = "Category is required")]
    public long CategoryId { get; set; }
}