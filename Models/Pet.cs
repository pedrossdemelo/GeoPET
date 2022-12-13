using System.ComponentModel.DataAnnotations.Schema;

namespace GeoPet.Models;

[Table("PETS")]
public class Pet
{
    public int PetId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }

    public string Size { get; set; } = string.Empty;

    public string Breed { get; set; } = string.Empty;

    public string? HashLocalization { get; set; } = string.Empty;

    public int PetCarerId { get; set; }
    
    public PetCarer? PetCarer { get; set; } = default!;
}
