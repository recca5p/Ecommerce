namespace Contract.DTOs.Response;

public sealed class CartItemDto
{
    public long CartItemId { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}
