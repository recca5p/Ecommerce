namespace Contract.DTOs.Response;

public sealed class OrderDto
{
    public long OrderId { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
}
