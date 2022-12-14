using System.ComponentModel.DataAnnotations;

namespace GeoPet.Models.Authorization;
public class AuthenticateRequest
{
    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}