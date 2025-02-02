using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class CategoryForCreationDto
{
    [Required(ErrorMessage = "Category name is required")]
    [MinLength(1)]
    public string CategoryName { get; set; }
}
