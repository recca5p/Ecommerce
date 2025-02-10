namespace Contract.DTOs.Response;

public sealed class OrderDto
{
    public long OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    public decimal TotalPrice { get; set; }

    public string PaymentStatus { get; set; }

    public string ShippingStatus { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; }
}
