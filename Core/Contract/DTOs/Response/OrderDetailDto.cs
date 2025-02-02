namespace Contract.DTOs.Response;

public sealed class OrderDetailDto
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
