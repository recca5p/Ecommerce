namespace Contract.DTOs.Response;

public sealed class PaymentDto
{
    public long PaymentId { get; set; }
    public long OrderId { get; set; }
    public string PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedDate { get; set; }
}
