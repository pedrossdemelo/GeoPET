using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GeoPet.Models.Authorization;

[ExcludeFromCodeCoverage]
public class AuthenticateRequest
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
    public string Password { get; set; } = default!;
}