using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class PaymentForCreationDto
{
    [Required]
    public long OrderId { get; set; }
    [Required]
    public string PaymentMethod { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }
}
