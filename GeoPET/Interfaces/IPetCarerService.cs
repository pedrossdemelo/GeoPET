using GeoPet.Entities;
using GeoPet.Models.Authorization;

namespace GeoPet.Interfaces;

public interface IPetCarerService
{
    Task<List<PetCarer>> GetAllPetCarers();
    Task<PetCarer?> GetCarer(int id);
    Task<PetCarer> GetPetCarerById(int id);
    Task<PetCarer> AddPetCarer(RegisterRequest body);
    Task<PetCarer> UpdatePetCarer(int id, UpdateRequest body);
    Task<bool> DeletePetCarer(int id);
    Task<List<Pet>> GetPetsByCarerId(int id);
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest body);
}

