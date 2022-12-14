using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GeoPet.Entities;
public class Breed
{
    [Key]
    public int BreedId { get; set; }

    [Required(ErrorMessage = "A pet must have a breed")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "A pet's breed must be between 3 and 40 characters long")]
    public string Name { get; set; } = default!;

    public virtual List<Pet> Pets { get; set; } = default!;
}