namespace Contract.DTOs.Response;

public sealed class ProductDetailDto
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public IEnumerable<ProductVariantDto> Variants { get; set; }
    public IEnumerable<ImageDto> Images { get; set; }
}
