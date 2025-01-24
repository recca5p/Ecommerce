namespace Contract.DTOs.Response;

public sealed class UserDto
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public long CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
}
