using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace GeoPet.Entities;

[Index(nameof(Email), IsUnique = true)]
public class PetCarer
{
    [Key]
    [JsonIgnore]
    public int PetCarerId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "ZipCode is required")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "ZipCode must have 8 digits")]
    public string ZipCode { get; set; } = default!;

    [JsonIgnore]
    public string PasswordHash { get; set; } = default!;

    public virtual List<Pet>? Pets { get; set; }
}
