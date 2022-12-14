using System.ComponentModel.DataAnnotations;

namespace GeoPet.Models.Authorization;

public class RegisterRequest
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "ZipCode is required")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "ZipCode must have 8 digits")]
    public string ZipCode { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
    public string Password { get; set; } = default!;
}