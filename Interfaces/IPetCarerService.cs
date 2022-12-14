using GeoPet.Entities;
using GeoPet.Models.Authorization;

namespace GeoPet.Interfaces;

public interface IPetCarerService
{
    Task<List<PetCarer>> GetAllPetCarers();
    Task<PetCarer> GetPetCarerById(int id);
    Task<PetCarer> AddPetCarer(RegisterRequest body);
    Task<PetCarer> UpdatePetCarer(int id, PetCarer body);
    Task<bool> DeletePetCarer(int id);
}

