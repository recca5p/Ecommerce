namespace Contract.DTOs.Response;

public sealed class OrderDetailDto
{
    public long VariantId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
