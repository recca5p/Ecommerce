using System.ComponentModel.DataAnnotations;

namespace Contract.DTOs.Request;

public sealed class UserForCreationDto
{
    [Required(ErrorMessage = "User name is required")]
    [MinLength(1)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; }
}
