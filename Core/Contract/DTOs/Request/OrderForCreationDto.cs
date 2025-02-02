using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class OrderForCreationDto
{
    [Required]
    public long UserId { get; set; }

    [Required]
    public IEnumerable<OrderDetailForCreationDto> OrderDetails { get; set; }
}
