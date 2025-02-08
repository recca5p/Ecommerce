namespace Contract.DTOs.Response;

public sealed class ProductDto
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string? Description { get; set; }
    public CategoryDto Category { get; set; }
    public List<ImageDto> Images { get; set; }
    public List<ReviewDto> Reviews { get; set; }
    public Int64 CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
}