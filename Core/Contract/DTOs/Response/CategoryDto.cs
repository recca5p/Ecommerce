namespace Contract.DTOs.Response;

public sealed class CategoryDto
{
    public long CategoryId { get; set; }
    public string CategoryName { get; set; }
    public long CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
}
