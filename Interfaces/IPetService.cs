using GeoPet.Entities;
using GeoPet.Models.Request;

namespace GeoPet.Interfaces;

public interface IPetService
{
    Task<List<Pet>> GetAllPets();
    Task<Pet> GetPetById(int id);
    Task<Pet> AddPet(PetRegisterRequest body);
    Task<Pet> UpdatePet(int id, PetRegisterRequest body);
    Task<List<Pet>> GetPetsByCarerId(int id);
    Task<bool> DeletePet(int id);
}
