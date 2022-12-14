using GeoPet.Data;
using GeoPet.Interfaces;
using GeoPet.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoPet.Services;

public class PetService : IPetService
{
    private readonly GeoPetContext _context;

    public PetService(GeoPetContext context) 
    {
        _context = context;
    }

    public async Task<Pet> AddPet(Pet body)
    {
        _context.Pets.Add(body);
        await _context.SaveChangesAsync();
        return body;
    }

    public async Task<bool> DeletePet(int id)
    {
        var searchPet = await _context.Pets.FindAsync(id);
        if (searchPet is null) throw new KeyNotFoundException("Pet not found");

        _context.Pets.Remove(searchPet);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Pet>> GetAllPets()
    {
        var result = await _context.Pets.ToListAsync();
        return result;
    }

    public async Task<Pet> GetPetById(int id)
    {
        var result = await _context.Pets.FindAsync(id);
        if (result is null) throw new KeyNotFoundException("Pet not found");

        return result;
    }

    public async Task<Pet> UpdatePet(int id, Pet body)
    {
        var searchPet = await _context.Pets.FindAsync(id);
        if (searchPet is null) throw new KeyNotFoundException("Pet not found");

        searchPet.Name = body.Name;
        searchPet.Age = body.Age;
        searchPet.Weight = body.Weight;
        searchPet.BreedId = body.BreedId;
        searchPet.PetCarerId = body.PetCarerId;
        searchPet.LocalizationHash = body.LocalizationHash; 

        await _context.SaveChangesAsync();
        return body;
    }
}

