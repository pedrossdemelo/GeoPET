using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GeoPet.Models;

[Index(nameof(Email), IsUnique = true)]
public class PetCarer
{
    [Key]
    public int PetCarerId { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is not valid")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "ZipCode is required")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "ZipCode must have 8 digits")]
    public string ZipCode { get; set; } = default!;
    
    [Required(ErrorMessage = "Password is required")]
    [Range(6, 20, ErrorMessage = "Password must be between 6 and 20 characters")]
    public string Password { get; set; } = default!;

    public virtual ICollection<Pet>? Pets { get; set; } = default!;
}
