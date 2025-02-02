namespace Contract.DTOs.Response;

public sealed class ProductVariantDto
{
    public long VariantId { get; set; }
    public string Color { get; set; }
    public string Storage { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public long ProductId { get; set; }
}
