using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class ImageForCreationDto
{
    [Required(ErrorMessage = "Image URL is required")]
    public string ImageUrl { get; set; }

    [Required(ErrorMessage = "IsPrimary is required")]
    public bool IsPrimary { get; set; }
    public long VariantId { get; set; }
}
