using GeoPet.Entities;

namespace GeoPet.Interfaces;

public interface IPetCarerService
{
    Task<List<PetCarer>> GetAllPetCarers();
    Task<PetCarer> GetPetCarerById(int id);
    Task<PetCarer> AddPetCarer(PetCarer body);
    Task<PetCarer> UpdatePetCarer(int id, PetCarer body);
    Task<bool> DeletePetCarer(int id);
}

