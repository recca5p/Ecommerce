namespace Contract.DTOs.Response;

public sealed class ImageDto
{
    public long ImageId { get; set; }
    public string ImageUrl { get; set; }
    public bool IsPrimary { get; set; }
    public long ProductId { get; set; }
}
