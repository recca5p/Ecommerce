namespace Contract.DTOs.Response;

public sealed class ProductDto
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string? Description { get; set; }
    public string Category { get; set; }
    public Int64 CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
}