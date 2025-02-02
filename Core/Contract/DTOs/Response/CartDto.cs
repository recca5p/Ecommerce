namespace Contract.DTOs.Response;

public sealed class CartDto
{
    public long CartId { get; set; }
    public long UserId { get; set; }
    public IEnumerable<CartItemDto> CartItems { get; set; }
}
