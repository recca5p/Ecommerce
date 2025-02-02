using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class CategoryForUpdateDto
{
    [Required(ErrorMessage = "Category name is required")]
    [MinLength(1)]
    public string CategoryName { get; set; }
}
