using System.Net;
using GeoPet.Data;
using GeoPet.Interfaces;
using GeoPet.Models;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace GeoPet.Services;

public class PetCarerService : IPetCarerService
{
    private async Task<bool> _validZipCode(string zipCode)
    {
        var client = new RestClient("https://viacep.com.br/ws/");
        var request = new RestRequest(zipCode + "/json", Method.Get);
        try {
            var response = await client.GetAsync(request);
            if (response.StatusCode == HttpStatusCode.OK) return true;
        } catch {
            return false;
        }
        return false;
    }
    private readonly GeoPetContext _context;

    public PetCarerService(GeoPetContext context)
    {
        _context = context;
    }

    public async Task<PetCarer?> AddPetCarer(PetCarer body)
    {
        if (!await _validZipCode(body.ZipCode.ToString())) return null;
        if (_context.PetCarers.AnyAsync(petCarer => petCarer.Email == body.Email).Result)
            return null;
        return body;
    }

    public async Task<bool> DeletePetCarer(int id)
    {
        var petCarer = await _context.PetCarers.FindAsync(id);

        if (petCarer is null) return false;

        _context.PetCarers.Remove(petCarer);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<PetCarer>> GetAllPetCarers()
    {
        var result = await _context.PetCarers.ToListAsync();

        return result;
    }

    public async Task<PetCarer?> GetPetCarerById(int id)
    {
        var petCarer = await _context.PetCarers.FindAsync(id);
        if (petCarer is null) return null;
        return petCarer;
    }

    public async Task<PetCarer?> UpdatePetCarer(int id, PetCarer body)
    {
        var petCarer = await _context.PetCarers.FindAsync(id);

        if (petCarer is null) return null;
        if (!await _validZipCode(body.ZipCode.ToString())) return null;
        if (body.Email != petCarer.Email && _context.PetCarers.AnyAsync(petCarer => petCarer.Email == body.Email).Result)
            return null;

        petCarer.Name = body.Name;
        petCarer.Email = body.Email;
        petCarer.ZipCode = body.ZipCode;
        petCarer.Password = body.Password;

        await _context.SaveChangesAsync();

        return petCarer;
    }
}

