using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class OrderForUpdateDto
{
    [Required]
    public string Status { get; set; }
}
