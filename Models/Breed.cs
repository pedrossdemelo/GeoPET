using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GeoPet.Models;
public class Breed
{
    [Key]
    public int BreedId { get; set; }

    [Required(ErrorMessage = "A pet must have a breed")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "A pet's breed must be between 3 and 40 characters long")]
    public string Name { get; set; } = default!;
}