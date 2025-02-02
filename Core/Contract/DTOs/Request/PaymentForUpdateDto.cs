using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class PaymentForUpdateDto
{
    [Required]
    public string PaymentMethod { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
    public decimal Amount { get; set; }
}
