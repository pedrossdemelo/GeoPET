namespace GeoPet.Interfaces;

using GeoPet.Entities;

public interface IJwtUtils
{
    public string GenerateToken(PetCarer user);
    public int? ValidateToken(string token);
}
