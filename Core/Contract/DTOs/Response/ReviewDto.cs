namespace Contract.DTOs.Response;

public sealed class ReviewDto
{
    public long ReviewId { get; set; }
    public long ProductId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedDate { get; set; }
}
