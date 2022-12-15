using GeoPet.Data;
using GeoPet.Interfaces;
using GeoPet.Entities;
using Microsoft.EntityFrameworkCore;
using GeoPet.Models.Request;

namespace GeoPet.Services;

public class PetService : IPetService
{
    private readonly GeoPetContext _context;

    private readonly IHttpContextAccessor _httpContextAccessor;
    public PetService(GeoPetContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Pet> AddPet(PetRegisterRequest body)
    {
        var petCarer = (PetCarer)_httpContextAccessor.HttpContext!.Items["PetCarer"]!;
        // map the body to a new pet
        var pet = new Pet
        {
            Name = body.Name,
            Age = body.Age,
            Weight = body.Weight,
            BreedId = body.BreedId,
            LocalizationHash = body.LocalizationHash,
            PetCarerId = petCarer.PetCarerId,
            PetCarer = petCarer
        };
        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();
        return pet;
    }

    public async Task<bool> DeletePet(int id)
    {
        var petCarer = (PetCarer)_httpContextAccessor.HttpContext!.Items["PetCarer"]!;
        var searchPet = await _context.Pets.FindAsync(id);
        if (searchPet is null) throw new KeyNotFoundException("Pet not found");
        if (petCarer.PetCarerId != searchPet.PetCarerId) throw new UnauthorizedAccessException("Unauthorized");

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

    public async Task<Pet> UpdatePet(int id, PetRegisterRequest body)
    {
        var petCarer = (PetCarer)_httpContextAccessor.HttpContext!.Items["PetCarer"]!;
        var searchPet = await _context.Pets.FindAsync(id);
        if (searchPet is null) throw new KeyNotFoundException("Pet not found");
        if (petCarer.PetCarerId != searchPet.PetCarerId) throw new UnauthorizedAccessException("Unauthorized");

        searchPet.Name = body.Name;
        searchPet.Age = body.Age;
        searchPet.Weight = body.Weight;
        searchPet.BreedId = body.BreedId;
        searchPet.LocalizationHash = body.LocalizationHash; 

        await _context.SaveChangesAsync();
        return searchPet;
    }

    public async Task<List<Pet>> GetPetsByCarerId(int id)
    {
        var petCarer = (PetCarer)_httpContextAccessor.HttpContext!.Items["PetCarer"]!;
        var result = await _context.Pets.Where(pet => pet.PetCarerId == petCarer.PetCarerId).ToListAsync();
        return result;
    }
}

