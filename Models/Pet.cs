using System.ComponentModel.DataAnnotations;

namespace GeoPet.Models;

public class Pet
{
    public int PetId { get; set; }

    [Required(ErrorMessage = "A pet must have a name")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "A pet's name must be between 1 and 50 characters long")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "A pet must have an age")]
    [Range(0, 50, ErrorMessage = "A pet's age must be between 0 and 50 years old")]
    public int Age { get; set; }

    [Required(ErrorMessage = "A pet must have a weight")]
    [Range(0.1, 100.00, ErrorMessage = "A pet's weight must be between 0.1 and 100.0 kg")]
    public double Weight { get; set; }

    public int? BreedId { get; set; }

    public virtual Breed? Breed { get; set; }

    public string? HashLocalization { get; set; }

    public int PetCarerId { get; set; }
    
    [Required(ErrorMessage = "A pet must have a carer")]
    public virtual PetCarer Carer { get; set; } = default!;
}
