using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class ReviewForCreationDto
{
    [Required]
    public long ProductId { get; set; }
    [Required]
    public long UserId { get; set; }
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }
    [Required]
    [MinLength(10)]
    public string Comment { get; set; }
}
