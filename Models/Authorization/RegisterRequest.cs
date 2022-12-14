using System.ComponentModel.DataAnnotations;

namespace GeoPet.Models.Authorization;

public class RegisterRequest
{
    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    public string Username { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}