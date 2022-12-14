using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GeoPet.Models.Authorization;

[ExcludeFromCodeCoverage]
public class UpdateRequest
{
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string? Email { get; set; } = default!;

    [StringLength(20, MinimumLength = 6, ErrorMessage = "New password must be between 6 and 20 characters")]
    public string? Password { get; set; } = default!;

    [StringLength(8, MinimumLength = 8, ErrorMessage = "New zip code must have 8 digits")]
    public string? ZipCode { get; set; } = default!;

    [StringLength(50, MinimumLength = 3, ErrorMessage = "New name must be between 3 and 50 characters")]
    public string? Name { get; set; } = default!;
}