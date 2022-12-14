using GeoPet.Entities;

namespace GeoPet.Interfaces;

public interface IPetService
{
    Task<List<Pet>> GetAllPets();
    Task<Pet> GetPetById(int id);
    Task<Pet> AddPet(Pet body);
    Task<Pet> UpdatePet(int id, Pet body);
    Task<bool> DeletePet(int id);
}
